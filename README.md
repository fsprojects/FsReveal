FsReveal [![Build status](https://ci.appveyor.com/api/projects/status/rpwg3epbvv5fwq1p/branch/master)](https://ci.appveyor.com/project/fsprojects/fsreveal/branch/master)
========

_Inspried by two great works:_

- [Reveal.js](http://lab.hakim.se/reveal-js/#/) by _Hakim El Hattab_
- [FSharp.Formatting](https://github.com/tpetricek/FSharp.Formatting) by _Tomas Petricek_
- FsReveal parses markdown and F# script file and generates reveal.js slides.

#### See FsReveal slides in action

- [1st sample](http://kimsk.github.io/fsreveal-sample-fsx/FsReveal.html#/)
  generated from
  [FsReveal.fsx](https://github.com/kimsk/fsreveal-sample-fsx/blob/master/slides/FsReveal.fsx)

- [2nd sample](http://kimsk.github.io/fsreveal-sample-md/FsReveal.html#/)
  generated from
  [FsReveal.md](https://github.com/kimsk/fsreveal-sample-md/blob/master/slides/FsReveal.md)
  ([raw](https://raw.githubusercontent.com/kimsk/fsreveal-sample-md/master/slides/FsReveal.md))

### Dev

    pyenv local 2.7.10
    # npm install -g yarn
    yarn install --ignore-engines
    yarn run build
    # when you want to test your changes:
    ./build.sh GenerateSlides

Source code is in `./src/FsReveal`. Built with `FAKE` and `yarn` (in turn uses `webpack`).

Source code is **also** in `./docs/tools` for the generation part.

Create a work-in-progress package by

 1. Create `paket.local` with
    `nuget FsReveal -> source /Users/h/dev/haf/FsReveal/temp version 1.4.0` but
    for your own home folder, in the `master` branch.
 2. `git checkout develop` to get to this branch.
 3. Hack on it
 4. `./build.sh NuGet`

#### How it all fits together

The node modules take care of bundling all the JS dependencies. You don't need
anything other than Yarn to compile, but some of them are dependent on having a
binary called 'python2' on your PATH.

`./src/FsReveal` contain the object model for transforming the Markdown and
literate F# files into slides.

`./src/FsReveal/template.{html,js}` is the entry point for webpack during
packaging.

When you've performed a change to the repo, it's all packaged as a NuGet package. That
NuGet now contains a copy of the compiled JS files, as well as
`./tools/generateSlides.fsx`.
