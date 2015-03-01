module FsReveal.SlidePropertySpecs

open FsReveal
open NUnit.Framework
open FsUnit

let md = """
- title : FsReveal
- description : Introduction to FsReveal
- author : Karlkim Suwanmongkol
- theme : Night
- transition : default

***
- background : image.png
- background-repeat : repeat

### Section 1

***
- background : image2.png

### Section 2

***

### Section 3"""

[<Test>]
let ``can read properties from slides``() = 
    let doc = md |> FsReveal.GetPresentationFromMarkdown
    let slideProperties = doc.Slides.[0].Properties
    slideProperties
    |> Seq.find (fun (k, _) -> k = "background")
    |> shouldEqual ("background", "image.png")
    slideProperties
    |> Seq.find (fun (k, _) -> k = "background-repeat")
    |> shouldEqual ("background-repeat", "repeat")

let md2 = """
- title : FsReveal
- description : Introduction to FsReveal
- author : Karlkim Suwanmongkol
- theme : Night
- transition : default

***
- no property
- background-repeat : repeat

### Section 1

***
- background : image2.png

### Section 2

***

### Section 3"""

[<Test>]
let ``can read properties from slides with list``() = 
    let doc = md2 |> FsReveal.GetPresentationFromMarkdown
    doc.Slides.[0].Properties |> shouldBeEmpty
