#load @"..\packages\FsReveal.0.0.4-beta\fsreveal\fsreveal.fsx"
open FsReveal

let outDir = @"c:\output"

open System.IO
let inputMd = Path.Combine( __SOURCE_DIRECTORY__, "input.md")
let inputFsx = Path.Combine( __SOURCE_DIRECTORY__, "input.fsx")


FsReveal.GenerateOutputFromMarkdownFile outDir "index.html" inputMd
FsReveal.GenerateOutputFromScriptFile outDir "index.html" inputFsx
