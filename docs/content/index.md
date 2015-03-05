# FsReveal

FsReveal allows you to write beautiful slides in Markdown and brings C# and F# to the [reveal.js][revealjs] web presentation framework.

## Features

- Write your slides in [Markdown](formatting.html) or .fsx files
- Syntax highlighting for most programming languages including C#, F# and LaTeX
- Speaker notes; Shows the current slide, next slide, elapsed time and current time
- Built in themes
- Horizontal and vertical slides
- Built in slide transitions using CSS 3D transforms
- Slide overview
- Works on mobile browsers. Swipe your way through the presentation.

## Getting started

* Read the [getting started tutorial](getting-started.html)
* Checkout the [formatting docs](formatting.html) to learn how to style your slides.

## Examples

Check out what others have created. Submit a PR if you have something to add to the list.

- [Markdown example][md-example] by [@kimsk][kimsk-twitter] [(source)][md-example-source]
- [.fsx example][fsx-example] by [@kimsk][kimsk-twitter] [(source)][fsx-example-source]
- [RPG F# Workshop][rpg-fsharp-workshop] by [@troykershaw][troykershaw-twitter] [(source)][rpg-fsharp-workshop-source]
- [F# on the Web - 0 to production in 12 weeks][fsharp-on-the-web] by [@panesofglass][panesofglass-twitter] [(source)][fsharp-on-the-web-source]

## Contributing and copyright

The project is hosted on [GitHub][gh] where you can [report issues][issues], fork 
the project and submit pull requests on the develop branch. If you're adding new public API, please also 
consider adding [samples][content] that can be turned into a documentation. You might
also want to read [library design notes][readme] to understand how it works.

The library is available under MIT license, which allows modification and 
redistribution for both commercial and non-commercial purposes. For more information see the 
[License file][license] in the GitHub repository. 


  [content]: https://github.com/fsprojects/FsReveal/tree/develop/docs/content
  [gh]: https://github.com/fsprojects/FsReveal
  [issues]: https://github.com/fsprojects/FsReveal/issues
  [readme]: https://github.com/fsprojects/FsReveal/blob/develop/README.md
  [license]: https://github.com/fsprojects/FsReveal/blob/develop/LICENSE.txt  
  
  [revealjs]: https://github.com/hakimel/reveal.js/ "reveal.js | HTML presentations made easy"
  
  [kimsk-twitter]: https://twitter.com/kimsk "@kimsk on Twitter"
  [troykershaw-twitter]: https://twitter.com/troykershaw "@troykershaw on Twitter"
  [panesofglass-twitter]: https://twitter.com/panesofglass "@panesofglass on Twitter"
  
  [fsx-example]: http://fsreveal.azurewebsites.net/ ".fsx example"
  [fsx-example-source]: https://github.com/fsprojects/FsReveal/blob/master/src/presentations/FsReveal.fsx ".fsx example source"
  
  [md-example]: http://fsreveal.azurewebsites.net/index-md.html "Markdown example"
  [md-example-source]: https://raw.githubusercontent.com/fsprojects/FsReveal/master/src/presentations/FsReveal.md "Markdown example source"
  
  [rpg-fsharp-workshop]: http://troykershaw.github.io/RpgFsharpWorkshop "RPG F# Workshop" 
  [rpg-fsharp-workshop-source]: https://github.com/troykershaw/RpgFsharpWorkshop "RPG F# Workshop source"
  
  [fsharp-on-the-web]: http://panesofglass.github.io/TodoBackendFSharp "F# on the Web - 0 to production in 12 weeks"
  [fsharp-on-the-web-source]: https://github.com/panesofglass/TodoBackendFSharp "F# on the Web source"
