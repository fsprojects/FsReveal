module FsReveal.SectionsSpecs

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

### Slide 1

***

### Slide 2

---

#### Slide 2.1

---

#### Sldie 2.2

***

### Slide 3"""

[<Test>]
let ``can generate sections from markdown``() = 
    let slides = (md |> FsReveal.GetPresentationFromMarkdown).Slides
    slides.Length |> shouldEqual 3
    let slide = 
        slides
        |> Seq.skip 1
        |> Seq.head
    match slide.SlideData with
    | SlideData.Nested x -> ()
    | _ -> failwith "subslides not parsed"

let md2 = """

***

### Slide 1

***

### Slide 2

---

#### Slide 2.1

---

#### Sldie 2.2

***

### Slide 3"""

[<Test>]
let ``can generate sections from markdown without properties``() = 
    let slides = (md2 |> FsReveal.GetPresentationFromMarkdown).Slides
    slides.Length |> shouldEqual 3
    let slide = 
        slides
        |> Seq.skip 1
        |> Seq.head
    match slide.SlideData with
    | SlideData.Nested x -> ()
    | _ -> failwith "subslides not parsed"
