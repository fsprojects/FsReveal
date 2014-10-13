(**
  If you want to get start quickly, just look at [Script.fsx](https://github.com/kimsk/FsReveal/blob/TryFsReveal/TryFsReveal/Script.fsx) in [TryFsReveal project](https://github.com/kimsk/FsReveal/tree/TryFsReveal).
  
  Or follow the quick sample on how to generate reveal.js slides using FsReveal below.

  First, add a reference to FsReveal.dll by loading FsReveal.fsx.
*)
#load @"..\..\packages\FsReveal\fsreveal\fsreveal.fsx"
open FsReveal
(**
  Then call either `FsReveal.generateOutputFromScriptFile` or `FsReveal.generateOutputFromMarkdownFile` functions.

  `outDir` is your slides output folder. After the slides are generated, FsReveal also copy all files required to host your reveal.js slides there.
*)
let outDir = @"c:\output"

(** input is an F# script file *)
FsReveal.GenerateOutputFromScriptFile outDir "index.html" "input.fsx"

(** input is a markdown file *)
FsReveal.GenerateOutputFromMarkdownFile outDir "index.html"  "input.md"