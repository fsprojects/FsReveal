#I @"../../FSharp.Formatting/lib/net40/"
#I @"../../FSharp.Compiler.Service/lib/net40/"
#I @"../../RazorEngine/lib/net40/"
#I @"../../Suave/lib/"
#I @"../lib/net40/"
#r "FSharp.Compiler.Service.dll"
#r "FSharp.Literate.dll"
#r "FsReveal.dll"
#r "suave.dll"

printfn "load FSharp.Formatting ..." 
printfn "load FSharp.Compiler.Service ..."
printfn "load FsReveal.dll"
 
FsReveal.FsRevealHelper.Folder <- __SOURCE_DIRECTORY__

printfn "Set FsReveal folder to %s" FsReveal.FsRevealHelper.Folder

open Suave
open Suave.Web
open Suave.Http
open Suave.Http.Files
open System.IO

let startWebServer outDir =
    let serverConfig = 
        { default_config with
           home_folder = Some (Path.Combine(FsReveal.FsRevealHelper.Folder, outDir))
        }

    let app =
        Writers.set_header "Cache-Control" "no-cache, no-store, must-revalidate"
        >>= Writers.set_header "Pragma" "no-cache"
        >>= Writers.set_header "Expires" "0"
        >>= browse
    web_server_async serverConfig app |> snd |> Async.Start
    Process.Start "http://localhost:8083/input.html" |> ignore