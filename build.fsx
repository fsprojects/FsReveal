#I @"packages/FsReveal/fsreveal/"
#I @"packages/FAKE/tools/"
#I @"packages/Suave/lib/net40"

#r "FakeLib.dll"
#r "Suave.dll"

#load "fsreveal.fsx"

// Git configuration (used for publishing documentation in gh-pages branch)
// The profile where the project is posted
let gitOwner = "myGitUser"
let gitHome = "https://github.com/" + gitOwner
// The name of the project on GitHub
let gitProjectName = "MyProject"
// The name of the GitHub repo subdirectory to publish slides to
let gitSubDir = ""

open FsReveal
open Fake
open Fake.Git
open System.IO
open System.Diagnostics
open Suave
open Suave.Web
open Suave.Http
open Suave.Operators
open Suave.Sockets
open Suave.Sockets.Control
open Suave.Sockets.AsyncSocket
open Suave.WebSocket
open Suave.Utils
open Suave.Files

let outDir = __SOURCE_DIRECTORY__ </> "output"
let slidesDir = getBuildParamOrDefault "input" (__SOURCE_DIRECTORY__ </> "slides")

Target "Clean" (fun _ ->
    CleanDirs [outDir]
)

let fsiEvaluator = 
    let evaluator = FSharp.Literate.FsiEvaluator()
    evaluator.EvaluationFailed.Add(fun err -> 
        traceImportant <| sprintf "Evaluating F# snippet failed:\n%s\nThe snippet evaluated:\n%s" err.StdErr err.Text )
    evaluator 

let copyStylesheet() =
    try
        CopyFile (outDir </> "css" </> "custom.css") (slidesDir </> "custom.css")
    with
    | exn -> traceImportant <| sprintf "Could not copy stylesheet: %s" exn.Message

let copyPics() =
    try
      CopyDir (outDir </> "images") (slidesDir </> "images") (fun f -> true)
    with
    | exn -> traceImportant <| sprintf "Could not copy picture: %s" exn.Message

let generateFor (file:FileInfo) = 
    try
        copyPics()
        let rec tryGenerate trials =
            try
                FsReveal.GenerateFromFile(file.FullName, outDir, fsiEvaluator = fsiEvaluator)
            with 
            | exn when trials > 0 -> tryGenerate (trials - 1)
            | exn -> 
                traceImportant <| sprintf "Could not generate slides for: %s" file.FullName
                traceImportant exn.Message

        tryGenerate 3

        copyStylesheet()
    with
    | :? FileNotFoundException as exn ->
        traceImportant <| sprintf "Could not copy file: %s" exn.FileName

let refreshEvent = new Event<_>()

let handleWatcherEvents (events:FileChange seq) =
    for e in events do
        let fi = fileInfo e.FullPath
        traceImportant <| sprintf "%s was changed." fi.Name
        match fi.Attributes.HasFlag FileAttributes.Hidden || fi.Attributes.HasFlag FileAttributes.Directory with
        | true -> ()
        | _ -> generateFor fi
    refreshEvent.Trigger()

let socketHandler (webSocket : WebSocket) =
  fun cx -> socket {
    while true do
      let! refreshed =
        Control.Async.AwaitEvent(refreshEvent.Publish)
        |> Suave.Sockets.SocketOp.ofAsync 
      do! webSocket.send Text (ASCII.bytes "refreshed") true
  }

let startWebServer () =
    let rec findPort port =
        let portIsTaken =
            if isMono then false else
            System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners()
            |> Seq.exists (fun x -> x.Port = port)

        if portIsTaken then findPort (port + 1) else port

    let port = findPort 8083

    let serverConfig = 
        { defaultConfig with
           homeFolder = Some (FullName outDir)
           bindings = [ HttpBinding.mkSimple HTTP "127.0.0.1" port ]
        }
    let app =
      choose [
        Filters.path "/websocket" >=> handShake socketHandler
        Writers.setHeader "Cache-Control" "no-cache, no-store, must-revalidate"
        >=> Writers.setHeader "Pragma" "no-cache"
        >=> Writers.setHeader "Expires" "0"
        >=> browseHome ]
    startWebServerAsync serverConfig app |> snd |> Async.Start
    Process.Start (sprintf "http://localhost:%d/index.html" port) |> ignore

Target "GenerateSlides" (fun _ ->
    !! (slidesDir </> "**" </> "*.md")
      ++ (slidesDir </> "**" </> "*.fsx")
    |> Seq.map fileInfo
    |> Seq.iter generateFor
)

Target "KeepRunning" (fun _ ->
    let watchSelection = slidesDir </> "**" </> "*.*"
    use watcher = !! watchSelection |> WatchChanges handleWatcherEvents
    
    startWebServer ()

    sprintf "Waiting for slide edits in %s. Press any key to stop." watchSelection |> traceImportant 

    System.Console.ReadKey() |> ignore

    watcher.Dispose()
)

Target "ReleaseSlides" (fun _ ->
    if gitOwner = "myGitUser" || gitProjectName = "MyProject" then
        failwith "You need to specify the gitOwner and gitProjectName in build.fsx"
    let tempDocsRoot = __SOURCE_DIRECTORY__ </> "temp/gh-pages"
    let tempDocsDir = tempDocsRoot </> gitSubDir
    CleanDir tempDocsRoot
    Repository.cloneSingleBranch "" (gitHome + "/" + gitProjectName + ".git") "gh-pages" tempDocsRoot

    fullclean tempDocsDir
    CopyRecursive outDir tempDocsDir true |> tracefn "%A"
    StageAll tempDocsRoot
    Git.Commit.Commit tempDocsRoot "Update generated slides"
    Branches.push tempDocsRoot
)

"Clean"
  ==> "GenerateSlides"
  ==> "KeepRunning"

"GenerateSlides"
  ==> "ReleaseSlides"
  
RunTargetOrDefault "KeepRunning"
