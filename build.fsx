#r @"packages/FAKE/tools/FakeLib.dll"

#load @"packages\FsReveal\fsreveal\fsreveal.fsx"

open FsReveal
open Fake

let outDir = "output"

Target "Clean" (fun _ ->
    CleanDirs [outDir]
)

Target "GenerateSlides" (fun _ ->
    !! "slides/*.md"
    |> Seq.iter (fun file -> 
            let fi = fileInfo file
            let outputFileName = fi.Name.Replace(fi.Extension,".html")
            FsReveal.GenerateOutputFromMarkdownFile outDir outputFileName file)
            
    !! "slides/*.fsx"
    |> Seq.iter (fun file -> 
            let fi = fileInfo file
            let outputFileName = fi.Name.Replace(fi.Extension,".html")
            FsReveal.GenerateOutputFromScriptFile outDir outputFileName file)            
)

Target "All" DoNothing

"Clean"
  ==> "GenerateSlides"
  ==> "All"
RunTargetOrDefault "All"
