(*@ title = F#Reveal @*)
(*@ description = Introduction to F#Reveal @*)
(*@ author = Karlkim Suwanmongkol @*)
(*@ theme = sky @*)
(*@ transition = default @*)
(*** hide ***)
#load @"G:\Fslab Journal\packages\FsLab.0.0.16\FsLab.fsx"
(*** slide-start ***)
(**
***
# F#Reveal
***
#### Karlkim Suwanmongkol
#### [@kimsk](http://twitter.com/kimsk)
#### [http://karlk.im](http://karlk.im)
#### [to@karlk.im](mailto:to@karlk.im)
*)
(*** slide-end ***)
(*** slide-start ***)
(*** slide-start ***)
(** 
## What is F#Reveal?

- [Reveal.js](http://lab.hakim.se/reveal-js/#/) by _Hakim El Hattab_
- [FSharp.Formatting](https://github.com/tpetricek/FSharp.Formatting) by _Tomas Petricek_
*)
(*** slide-end ***)
(*** slide-start ***)
(**
### Reveal.js

- A framework for easily creating beautiful presentations using HTML.  
  

> **Atwood's Law**: any application that can be written in JavaScript, will eventually be written in JavaScript.
*)
(*** slide-end ***)
(*** slide-start ***)
(**
### FSharp.Formatting

- F# tools for generating documentation (Markdown processor and F# code formatter).
  - [Literate F# script file](http://tpetricek.github.io/FSharp.Formatting/sidescript.html) (markdown & F# code in fsx file)
  - [Literate Markdown file](http://tpetricek.github.io/FSharp.Formatting/sidemarkdown.html) (markdown & F# code in md file)
  - Generate HTML, PDF, reveal.js, etc.
  - Markdown extensions : LaTeX
  - Code syntax highlighting support.

*)
(*** slide-end ***)
(*** slide-start ***)
(**
### FSharp.Formatting

- Evaluate F# code using [FSharp.Compiler.Service](http://fsharp.github.io/FSharp.Compiler.Service/)
- [FsLab](https://github.com/tpetricek/FsLab): [sample](http://kimsk.github.io/Intro-to-Statistics/)
- Used by many F# and non-F# opensource projects
*)
(*** slide-end ***)
(*** slide-start ***)
(**
###F#Reveal

- F#Reveal links FSharp.Formatting and reveal.js
- It parses literate F# script file and generates reveal.js slides!
*)
// Trying to explain F#Reveal in F#
let Markdown = "markdown"
let ``F# code`` = "F# code"

type RevealJs = 
  { 
    html: string
    tooltips: string
  }

let FsReveal = fun m f -> { html = "html"; tooltips = "tooltips" }
let output = (Markdown, ``F# code``) ||> FsReveal
(**
The value of the output is..
*)
(*** include-value: output ***)
(** 
Try hovering the F# code with your mouse..
*)
(*** slide-end ***)
(*** slide-end ***)
(*** slide-start ***)
(*** slide-start ***)
(**
## Why F#Reveal?

- F# Type inference
  - Don't need to specify type (most of the time)
  - Less verbose and **easy** to write
  - Can be **hard** to read without help from an IDE
*)
(*** slide-end ***)
(*** slide-start ***)
(**
## Why F#Reveal?

- Create your slides using markdown syntax
- Syntax highlighting for many programming languages (not only F#)
- LaTeX
- F# code is evaluated
  - Display values
  - Charting and so on
*)
(*** slide-end ***)
(*** slide-end ***)
(*** slide-start ***)
(*** slide-start ***)
(** 
## Card Game Sample

> [Domain Driven Design with F# type System](http://www.slideshare.net/ScottWlaschin/ddd-with-fsharptypesystemlondonndc2013) by *Scott Wlaschin*
*)
(*** slide-end ***)
(*** slide-start ***)
(** 
#### F# Type Definitions
*)
type Suit = Club|Diamond|Spade|Heart
type Rank = Two|Three|Four|Five|Six|Seven|Eight
            |Nine|Ten|Jack|Queen|King|Ace
type Card = Suit * Rank   
type Person = { Name: string; Cards: seq<Card>}
(*** slide-end ***)
(*** slide-start ***)
(**
### Some random F# function
*)
let pickHighRanks player suit =
  player.Cards 
  |> Seq.filter (fun c -> fst(c) = suit)
  |> Seq.filter(fun (s,r) -> 
      match r with
      | Ace
      | King
      | Queen
      | Jack -> true
      | _ -> false  )
(** 
Need to think like a compiler to understand the code above..
*)
(*** slide-end ***)
(*** slide-end ***)
(*** slide-start ***)
(*** slide-start ***)
(**
### C#
    [lang=cs]
    using System;
 
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello, world!");
        }
    }
*)
(*** slide-end ***)
(*** slide-start ***)
(**
### JavaScript
 
    [lang=js]
    function copyWithEvaluation(iElem, elem) {
        return function (obj) {
            var newObj = {};
            for (var p in obj) {
                var v = obj[p];
                if (typeof v === "function") {
                    v = v(iElem, elem);
                }
                newObj[p] = v;
            }
            if (!newObj.exactTiming) {
                newObj.delay += exports._libraryDelay;
            }
            return newObj;
        };
    }
    
*code from [winjs](https://github.com/winjs/winjs/blob/master/src/js/WinJS/Animations/_TransitionAnimation.js) project*
*)
(*** slide-end ***)
(*** slide-start ***)
(**
### Haskell
 
    [lang=haskell]
    recur_count k = 1 : 1 : zipWith recurAdd (recur_count k) (tail (recur_count k))
            where recurAdd x y = k * x + y

    main = do
      argv <- getArgs
      inputFile <- openFile (head argv) ReadMode
      line <- hGetLine inputFile
      let [n,k] = map read (words line)
      printf "%d\n" ((recur_count k) !! (n-1))

*code from [NashFP/rosalind](https://github.com/NashFP/rosalind/blob/master/mark_wutka%2Bhaskell/FIB/fib_ziplist.hs)*
*)
(*** slide-end ***)
(*** slide-start ***)
(**
### SQL
 
    [lang=sql]
    select * 
    from 
      (select 1 as Id union all select 2 union all select 3) as X 
    where Id in (@Ids1, @Ids2, @Ids3)

*sql from [Dapper](https://code.google.com/p/dapper-dot-net/)* 
*)
(*** slide-end ***)
(*** slide-end ***)
(*** slide-start ***)
(** 
### Define some values
*)
[<Measure>] type sqft
[<Measure>] type dollar
let sizes = [|1700<sqft>;2100<sqft>;1900<sqft>;1300<sqft>;1600<sqft>;2200<sqft>|]
let prices = [|53000<dollar>;44000<dollar>;59000<dollar>;82000<dollar>;50000<dollar>;68000<dollar>|] 
(*** slide-end ***)
(*** slide-start ***)
(** 
### F# code is evaluated

#### `sizes.[0]`
*)
(*** include-value: sizes.[0] ***)
(*** slide-end ***)
(*** slide-start ***)
(**
### Calculate price/sqft
*)
let getPricePerSqft size price = (float price)/(float size)
let ``price per sqft`` = (sizes,prices) ||> Array.map2 getPricePerSqft
(*** include-value: ``price per sqft`` ***)
(*** slide-end ***)
(*** slide-start ***)
(**
**We can include table (`deedle frame`)**
*)
open Deedle
let df = Frame(["size"; "price"],  [ Series.ofValues sizes; Series.ofValues prices])
(*** include-value:df ***)
(*** slide-end ***)
(*** slide-start ***)
(**
**Or a chart drawn by `FSharp.Charting`**
*)
(*** define-output:chart1 ***)
open FSharp.Charting 
Chart.Point(Seq.zip sizes prices)
(*** include-it:chart1 ***)
(*** slide-end ***)
(*** slide-start ***)
(** 
** More chart..
*)
let data = [3;4;2;4;3;5;3;6;4;3]
(*** define-output:chart2 ***)
data 
|> Seq.groupBy id 
|> Seq.map (fun (l,v) -> l.ToString(),(Seq.length v))
|> Seq.sortBy fst
|> Chart.Column
(*** include-it:chart2 ***)
(*** slide-end ***)
(*** slide-start ***)
(**
**Bayes' Rule in LaTeX**
  
$ \Pr(A|B)=\frac{\Pr(B|A)\Pr(A)}{\Pr(B|A)\Pr(A)+\Pr(B|\neg A)\Pr(\neg A)} $
*)
(*** slide-end ***)
(*** slide-start ***)
(**
### The Reality of a Developer's Life 

**When I show my boss that I've fixed a bug:**
  
![When I show my boss that I've fixed a bug](http://www.topito.com/wp-content/uploads/2013/01/code-07.gif)
  
**When your regular expression returns what you expect:**
  
![When your regular expression returns what you expect](http://www.topito.com/wp-content/uploads/2013/01/code-03.gif)
  
*from [The Reality of a Developer's Life - in GIFs, Of Course](http://server.dzone.com/articles/reality-developers-life-gifs)*
*)
(*** slide-end ***)
(*** slide-start ***)
(**
## Thank you for coming!

- [F# Logo Project](https://www.surveymonkey.com/s/FSharpLogo)
- [The F# Software Foundation](http://fsharp.org/)
- [Community for F#](http://c4fsharp.net/) 
- [#fsharp](https://twitter.com/search?src=typd&q=%23fsharp) on twitter
- [Nashville F# User Group](http://www.meetup.com/nashfsharp/) & [F# Thai](https://www.facebook.com/groups/425426147563091/)
- [F#Reveal](https://github.com/kimsk/FsReveal) (POC) on GitHub

*)
(*** slide-end ***)