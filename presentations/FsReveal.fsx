(**
- title : FsReveal 
- description : Introduction to FsReveal
- author : Karlkim Suwanmongkol
- theme : dark
- transition : default

***

### What is FsReveal?

- Generates [reveal.js](http://lab.hakim.se/reveal-js/#/) presentation from [markdown](http://daringfireball.net/projects/markdown/)
- Utilizes [FSharp.Formatting](https://github.com/tpetricek/FSharp.Formatting) for markdown parsing

***

### Reveal.js

- A framework for easily creating beautiful presentations using HTML.  
  

> **Atwood's Law**: any application that can be written in JavaScript, will eventually be written in JavaScript.

***

### FSharp.Formatting

- F# tools for generating documentation (Markdown processor and F# code formatter).
- It parses a literate F# script file (markdown & F# code) and generate HTML or PDF.
- Code syntax highlighting support.
- It also evaluates your F# code and produce tooltips.

***

# Slide-1

## test

*)
let a = 10
let b = 20
let c = a + b
(**
---

# Sub-Slide-1

## test

---

# Sub-Slide-2

## test

***

# Slide-2

*)