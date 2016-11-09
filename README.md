FsReveal [![Build status](https://ci.appveyor.com/api/projects/status/rpwg3epbvv5fwq1p/branch/master)](https://ci.appveyor.com/project/fsprojects/fsreveal/branch/master)
========

_Inspried by two great works:_

- [Reveal.js](http://lab.hakim.se/reveal-js/#/) by _Hakim El Hattab_
- [FSharp.Formatting](https://github.com/tpetricek/FSharp.Formatting) by _Tomas Petricek_
- FsReveal parses markdown and F# script file and generates reveal.js slides.

#### See FsReveal slides in action

- [1st sample](http://kimsk.github.io/fsreveal-sample-fsx/FsReveal.html#/) generated from [FsReveal.fsx](https://github.com/kimsk/fsreveal-sample-fsx/blob/master/slides/FsReveal.fsx)
- [2nd sample](http://kimsk.github.io/fsreveal-sample-md/FsReveal.html#/) generated from [FsReveal.md](https://github.com/kimsk/fsreveal-sample-md/blob/master/slides/FsReveal.md) ([raw](https://raw.githubusercontent.com/kimsk/fsreveal-sample-md/master/slides/FsReveal.md))

### Dev

```
pyenv local 2.7.10
# npm install -g yarn
yarn install --ignore-engines
yarn run build
```

Source code is in `./src/FsReveal`. Built with `FAKE` and `yarn` (in turn uses `webpack`).
