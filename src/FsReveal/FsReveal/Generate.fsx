#load "../packages/FsLab.0.0.16/FsLab.fsx"

#I "../../FSharp.Formatting/bin"
#r "FSharp.Literate.dll"
#r "FSharp.CodeFormat.dll"
#r "FSharp.MetadataFormat.dll"
#r "FSharp.Markdown.dll"
#r "RazorEngine.dll"
#r "FSharp.Compiler.Service.dll"
#r "CSharpFormat.dll"

#load "Formatters.fs"
#load "FsReveal.fs"

open FSharp.Literate
open FSharp.Markdown
open FSharp.CodeFormat
open System.Collections.Generic
open CSharpFormat
open System
open System.IO
open System.Web
open FsReveal

let fsx = Path.Combine(__SOURCE_DIRECTORY__, "../../../fsreveal.fsx")
let outDir = Path.Combine(__SOURCE_DIRECTORY__, "../../../output")

File.ReadAllText(fsx)
|> FsReveal.ProcessScriptFile  outDir "index.html"
