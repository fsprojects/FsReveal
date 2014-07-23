(**
  Below is a quick sample on how to generate reveal.js slides using FsReveal.

  First, add a reference to FsReveal.dll
*)
#r "FsReveal.dll"
open FsReveal
(**
  Then call either `FsReveal.generateOutputFromScriptFile` or `FsReveal.generateOutputFromMarkdownFile` functions.

  `outDir` is the output folder that contains all files required to host your reveal.js slides.   
*)
let outDir = @"c:\output"

(** input is an F# script file *)
FsReveal.GenerateOutputFromScriptFile outDir "index.html" "input.fsx"

(** input is a markdown file *)
FsReveal.GenerateOutputFromMarkdownFile outDir "index.html"  "input.md"
