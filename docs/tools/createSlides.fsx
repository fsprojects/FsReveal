#I @"../../bin/"
#r "FsReveal.dll"
#r "FSharp.Literate.dll"
#r "../../packages/build/FAKE/tools/FakeLib.dll"
open Fake
open System.IO
open FsReveal
let combine args = Path.Combine (Array.ofList args)
let exists file = File.Exists file
let copy source target = File.Copy(source, target)
let root = Path.Combine(__SOURCE_DIRECTORY__,"../../")
let rooted file = combine [ root; file ]
let slidesDir = root @@ "/docs/slides"
let outDir = root @@ "/docs/output/samples/"
FsReveal.FsRevealHelper.DistFolder <- rooted "dist"
FsReveal.FsRevealHelper.TemplateFile <- rooted "dist/index.html"

let targetFCIS = rooted @"lib/FSharp.Compiler.Interactive.Settings.dll"

if not (exists targetFCIS) then
  copy (rooted @"bin/FSharp.Compiler.Interactive.Settings.dll") targetFCIS

let copyStylesheet () =
  try
    CopyFile (outDir @@ "css/custom.css") (slidesDir @@ "custom.css")
  with
  | exn ->
    traceImportant <| sprintf "Could not copy stylesheet: %s" exn.Message

let copyPics() =
  try
    !! (slidesDir @@ "images/*.*")
    |> CopyFiles (outDir @@ "images")
  with
  | exn ->
    traceImportant <| sprintf "Could not copy picture: %s" exn.Message

let generateFor (file:FileInfo) =
  let rec tryGenerate trials =
    try
      FsReveal.GenerateFromFile(file.FullName, outDir)
    with
    | exn when trials > 0 -> tryGenerate (trials - 1)
    | exn ->
      traceImportant <| sprintf "Could not generate slides for: %s" file.FullName
      traceImportant exn.Message
  try
    copyPics()
    tryGenerate 3
    copyStylesheet()
  with
  | :? FileNotFoundException as exn ->
    traceImportant <| sprintf "Could not copy file: %s" exn.FileName

!! (slidesDir @@ "*.md")
    ++ (slidesDir @@ "*.fsx")
|> Seq.map fileInfo
|> Seq.iter generateFor