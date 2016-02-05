### 1.2.1 - 05.02.2016
* Use FSharp.Formatting 2.14.0 

### 1.2.0 - 31.01.2016
* Slide properties also allow to name slide that you can reference in links - http://fsprojects.github.io/FsReveal/formatting.html#Named-slides
* Syntax highlighting for Paket files
* Fix auto-refresh - https://github.com/fsprojects/FsReveal/pull/88
* Updated all dependencies

### 1.1.0 - 10.11.2015
* Updated to reveal.js 3.2.0
* Update FSharp.Formatting to 2.12.0
* Fix CSS around code

### 1.0.0 - 14.06.2015
* Use highlight.js 8.6 from our own fork - allows 125 languages and 63 styles
* Auto-Update slides in Edit mode - https://github.com/fsprojects/FsReveal/pull/73
* Do not double-format already highlighted code

### 0.8.0 - 01.06.2015
* Using reveal.js 3.1

### 0.7.7 - 06.05.2015
* Fix package

### 0.7.3 - 02.05.2015
* Using latest dependencies and fixed FSharp.Formatting

### 0.7.2 - 22.03.2015
* BUGFIX: Fixing dependencies

### 0.7.0 - 11.03.2015
* BREAKING API: Use optional parameters and allow speechifying FSI evaluator - https://github.com/fsprojects/FsReveal/pull/51

### 0.6.3 - 08.03.2015
* Fix slide properties

### 0.6.0 - 05.03.2015
* Simpler way of defining speaker notes in markdown - https://github.com/fsprojects/FsReveal/pull/48
* Allow to use a custom css file - https://github.com/fsprojects/FsReveal/pull/45
* Use FsReveal for demos in the FsReveal docs - http://fsprojects.github.io/FsReveal/formatting.html

### 0.5.0 - 02.03.2015
* Use FSharp.Formatting 2.7.4 and update to .NET 4.5
* Update default F# Formatting CSS (to support type/DU/module distinctions)
* Fix issues with processing (and evaluating) fsx scripts
* Use reveal.js 3.0.0 - https://github.com/hakimel/reveal.js/releases/tag/3.0.0
* BUGFIX: Don't delete index.html
 
### 0.2.2 - 12.01.2015
* Use FSharp.Formatting 2.6

### 0.2.1 - 05.01.2015
* BUGFIX: Detect subslides

### 0.2.0 - 05.01.2015
* First copy reveal.js then overwrite with slides
* Update all dependencies

### 0.1.8 - 14.11.2014
* Allow to add slide properties

### 0.1.0 - 12.11.2014
* GitHub release
* Use scheme relative urls in template
* Remove fixed version of FSharp.Formatting and other packages
* Retrieve reveal.js via paket
* Create output folder if it does not exist.
* Check if the input file exists to avoid throwing Exception.
* Using FSharp.Formatting and FSharp.Compiler.Service
* Initial release 
