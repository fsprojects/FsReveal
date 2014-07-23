(**
### See FsReveal slides in action

- [1st sample](http://fsreveal.azurewebsites.net/) generated from [FsReveal.fsx](https://github.com/kimsk/FsReveal/blob/master/src/presentations/FsReveal.fsx)
- [2nd sample](http://fsreveal.azurewebsites.net/index-md.html) generated from [FsReveal.md](https://github.com/kimsk/FsReveal/blob/master/src/presentations/FsReveal.md) ([raw](https://raw.githubusercontent.com/kimsk/FsReveal/master/src/presentations/FsReveal.md))

***

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
(** 
Contributing and copyright
--------------------------

The project is hosted on [GitHub][gh] where you can [report issues][issues], fork 
the project and submit pull requests. If you're adding new public API, please also 
consider adding [samples][content] that can be turned into a documentation. You might
also want to read [library design notes][readme] to understand how it works.

The library is available under Public Domain license, which allows modification and 
redistribution for both commercial and non-commercial purposes. For more information see the 
[License file][license] in the GitHub repository. 

  [content]: https://github.com/kimsk/FsReveal/tree/master/docs/content
  [gh]: https://github.com/kimsk/FsReveal
  [issues]: https://github.com/kimsk/FsReveal/issues
  [readme]: https://github.com/kimsk/FsReveal/blob/master/README.md
  [license]: https://github.com/kimsk/FsReveal/blob/master/LICENSE.txt
*)
