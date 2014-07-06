(*@ title = F#Reveal @*)
(*@ description = Introduction to F#Reveal @*)
(*@ author = Karlkim Suwanmongkol @*)
(*@ theme = sky @*)
(*@ transition = default @*)
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
(*** hide ***)
#load "./src/FsReveal/packages/FsLab.0.0.16/FsLab.fsx"
let Markdown = "markdown"
let ``F# code`` = "code"
type Awesome = { name: string}
let FsReveal markdown fsharp = 
  "awesome"
(** 
## What is F#Reveal?

- [Reveal.js](http://lab.hakim.se/reveal-js/#/) by _Hakim El Hattab_
- [FSharp.Formatting](https://github.com/tpetricek/FSharp.Formatting) by _Tomas Petricek_  
- F#Reveal parses fsx file (markdown & F# code) and generates reveal.js slides!
  
*)
// Say it in F#
let output = (Markdown, ``F# code``) ||> FsReveal
(** 
**Try hovering the F# code with your mouse..**
*)
(*** slide-end ***)
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
(*** slide-start ***)
(** 
## Card Game Sample

> Domain Driven Design with F# type System by *Scott Wlaschin*
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
## More to come..
*)
(*** slide-end ***)