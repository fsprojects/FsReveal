(**
  Below is a quick sample on how to generate reveal.js slides using FsReveal.

  First, add a reference to FsReveal.dll
*)
#r @"..\..\bin\FsReveal.dll"
(**
  Then call either `FsReveal.generateOutputFromScriptFile` or `FsReveal.generateOutputFromMarkdownFile` functions.

  `outDir` is the output folder that contains all files required.

*)
let outDir = @"g:\output"

FsReveal.generateOutputFromScriptFile outDir "test-fsx.html" "input.fsx"

FsReveal.generateOutputFromMarkdownFile outDir "test-md.html"  "input.md"

