(**
### See FsReveal slides in action

- [1st sample](http://fsreveal.azurewebsites.net/) generated from [FsReveal.fsx](https://github.com/kimsk/FsReveal/blob/master/src/presentations/FsReveal.fsx)
- [2nd sample](http://fsreveal.azurewebsites.net/index-md.html) generated from [FsReveal.md](https://github.com/kimsk/FsReveal/blob/master/src/presentations/FsReveal.md) ([raw](https://raw.githubusercontent.com/kimsk/FsReveal/master/src/presentations/FsReveal.md))

***

  <div class="well well-small center" id="nuget">
      The FsReveal can be
      <a href="https://www.nuget.org/packages/FsReveal">installed from NuGet</a>:
      <pre>PM> Install-Package FsReveal -Pre</pre>
  </div>

  If you want to get start quickly, just look at [Script.fsx](https://github.com/kimsk/FsReveal/blob/TryFsReveal/TryFsReveal/Script.fsx) in [TryFsReveal project](https://github.com/kimsk/FsReveal/tree/TryFsReveal).
  
  Or follow the quick sample on how to generate reveal.js slides using FsReveal below.

  First, load FSharp.Formatting, FSharp.Compiler.Service, and FsReveal.fsx.
*)
#I @"..\..\FSharp.Formatting.2.4.18\lib\net40\"
#I @"..\..\FSharp.Compiler.Service.0.0.57\lib\net40\"
#r "FSharp.Compiler.Service.dll"
#r "FSharp.Literate.dll"
#load @"..\packages\FsReveal.0.0.3-beta\fsreveal\fsreveal.fsx"
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

Logo
----

- [ProjectScaffold](https://github.com/fsprojects/ProjectScaffold/blob/9e28426459007df785432fca4cf8996b0aed90d0/docs/files/img/logo-template.pdn) by Sergey Tihon
- [Presentation icon](http://thenounproject.com/term/presentation/47356/) by [Milky](http://thenounproject.com/Milky/)


  [content]: https://github.com/kimsk/FsReveal/tree/master/docs/content
  [gh]: https://github.com/kimsk/FsReveal
  [issues]: https://github.com/kimsk/FsReveal/issues
  [readme]: https://github.com/kimsk/FsReveal/blob/master/README.md
  [license]: https://github.com/kimsk/FsReveal/blob/master/LICENSE.txt  
*)
