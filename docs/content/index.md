# FsReveal

FsReveal allows you to write beautiful slides in Markdown and brings F# to the [reveal.js][revealjs] web presentation framework.

## Features

- Write your slides in [Markdown](http://daringfireball.net/projects/markdown/syntax) or .fsx files
- Syntax highlighting for most programming languages including C#, F# and LaTeX
- Speaker notes; Shows the current slide, next slide, elapsed time and current time
- Built in themes
- Horizontal and vertical slides
- Built in slide transitions using CSS 3D transforms
- Slide overview
- Works on mobile browsers. Swipe your way through the presentation.

## Getting Started

Clone or [download](https://github.com/fsprojects/FsReveal/archive/master.zip) the [FsReveal repository](https://github.com/fsprojects/FsReveal). Check out GitHub's links to the right if you need some help.

### Prerequisites
If you are using Linux or OS X, [you need to install Mono](http://www.mono-project.com/download/ "Install Mono").

Windows users don't need to install anything.

### Create

Open `slides/input.md` in your favourite text editor.

This is the source for your entire presentation. Inside you will find an example presentation that demonstrates how to use FsReveal.

#### Images

Put images into the slides/images folder.

### Styling

Create a custom stylesheet in `slides/custom.css`.

### Build

Open a console/terminal in the FsReveal folder.

If you're using Windows run 
    
    build.cmd
    
If you're using a Mac run

    ./build.sh
    
This will download all of the packages that FsReveal needs to create your slides.

Your slides are then generated and saved to the `output` folder.

A web server will start automatically and your presentation will be opened in your browser.

FsReveal will detect changes to your slides and generate them again for you; you just need to refresh your browser.

> We're working on auto refreshing the browser for you.

### Use

- Use the arrow keys to navigate left, right, up and down
- Press `Esc` to see an overview
- Press `f` to view in fullscreen
- Press `s` to see speaker notes.

### Deployment

GitHub allows to host your slides. In order to deploy your slides make sure you have a `gh-pages` branch. 
If it doesn't exist, then just create and push it.
After that you can release your software by simply running:
     
    $ build ReleaseSlides

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

The library is available under Public Domain license, which allows modification and 
redistribution for both commercial and non-commercial purposes. For more information see the 
[License file][license] in the GitHub repository. 

Logo
----

- [ProjectScaffold](https://github.com/fsprojects/ProjectScaffold/blob/9e28426459007df785432fca4cf8996b0aed90d0/docs/files/img/logo-template.pdn) by Sergey Tihon
- [Presentation icon](http://thenounproject.com/term/presentation/47356/) by [Milky](http://thenounproject.com/Milky/)


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