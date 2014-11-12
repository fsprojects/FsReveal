## See FsReveal slides in action

- [1st sample](http://fsreveal.azurewebsites.net/) generated from [FsReveal.fsx](https://github.com/fsprojects/FsReveal/blob/develop/src/presentations/FsReveal.fsx)
- [2nd sample](http://fsreveal.azurewebsites.net/index-md.html) generated from [FsReveal.md](https://github.com/fsprojects/FsReveal/blob/develop/src/presentations/FsReveal.md)

## Getting Started

### Get
Clone or download this repo. Check out GitHub's links to the right if you need some help.

### Create
Open `slides/input.md` in your favourite text editor.

This is the source for your entire presentation. Inside you will find an example presentation that demonstrates how to use FsReveal.

### Build
Open a console/terminal and `cd` into the FsReveal folder.

If you're using Windows run 
    
    build.cmd
    
If you're using a Mac run

    ./build.sh
    
This will download all of the packages that FsReveal needs to create your slides.

Whenever a change is detected the slides are automatically regenerated and updated in the `output` folder.
Use your favorite webbrowser to view the slides.  

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