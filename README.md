[![Issue Stats](http://issuestats.com/github/fsprojects/FsReveal/badge/issue)](http://issuestats.com/github/fsprojects/FsReveal)
[![Issue Stats](http://issuestats.com/github/fsprojects/FsReveal/badge/pr)](http://issuestats.com/github/fsprojects/FsReveal)

# FsReveal [![NuGet Status](http://img.shields.io/nuget/v/FsReveal.svg?style=flat)](https://www.nuget.org/packages/FsReveal/)

FsReveal allows you to write beautiful slides in
[Markdown](http://daringfireball.net/projects/markdown/syntax) and brings F# to
the [reveal.js][revealjs] web presentation framework.

## Quickstart

    git clone --depth=1 https://github.com/fsprojects/FsReveal.git
    cd FsReveal
    ./build.sh

## Features

- Write your slides in
  [Markdown](http://daringfireball.net/projects/markdown/syntax) or .fsx files
- Automatically updates the browser in edit mode on every save
- Syntax highlighting for most programming languages including C#, F# and LaTeX
- Speaker notes; Shows the current slide, next slide, elapsed time and current
  time
- Built in themes
- Horizontal and vertical slides
- Built in slide transitions using CSS 3D transforms
- Slide overview
- Works on mobile browsers. Swipe your way through the presentation.

[Examples](http://fsprojects.github.io/FsReveal/index.html#Examples) and a
[Getting started
guide](http://fsprojects.github.io/FsReveal/getting-started.html) can be found
in the docs.

[revealjs]: https://github.com/hakimel/reveal.js/ "reveal.js | HTML presentations made easy"

### Hacking on the project

    git checkout develop
    # follow README in branch
    # git commit ...
    # send PR to `develop` if you're improving `FsReveal` or to `master` if you're
    # changing the 'default' presentation everyone starts off with.

### Maintainer(s)

- [@kimsk](https://github.com/kimsk)
- [@forki](https://github.com/forki)
- [@troykershaw](https://github.com/troykershaw)
