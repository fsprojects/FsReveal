(**
### See FsReveal slides in action

- [1st sample](http://fsreveal.azurewebsites.net/) generated from [FsReveal.fsx](https://github.com/fsprojects/FsReveal/blob/master/src/presentations/FsReveal.fsx)
- [2nd sample](http://fsreveal.azurewebsites.net/index-md.html) generated from [FsReveal.md](https://github.com/fsprojects/FsReveal/blob/master/src/presentations/FsReveal.md) ([raw](https://raw.githubusercontent.com/fsprojects/FsReveal/master/src/presentations/FsReveal.md))

**)

  <div class="well well-small center" id="nuget">
      The FsReveal can be
      <a href="https://www.nuget.org/packages/FsReveal">installed from NuGet</a>:
      <pre>PM> Install-Package FsReveal</pre>
  </div>

  If you want to get start quickly, just checkout the [TryFsReveal branch](https://github.com/fsprojects/FsReveal/tree/TryFsReveal).
  
  Run `build.cmd KeepRunning` (or `build.sh KeepRunning` on mono) and you can start to edit the slides in the `slides` folder.
  
  Whenever a change is detected the slides are automatically regenerated and updated in the `output` folder.
  Use your favorite webbrowser to view the slides.  
   
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


  [content]: https://github.com/fsprojects/FsReveal/tree/master/docs/content
  [gh]: https://github.com/fsprojects/FsReveal
  [issues]: https://github.com/fsprojects/FsReveal/issues
  [readme]: https://github.com/fsprojects/FsReveal/blob/master/README.md
  [license]: https://github.com/fsprojects/FsReveal/blob/master/LICENSE.txt  
*)
