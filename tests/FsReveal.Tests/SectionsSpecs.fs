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

### Section 1

***

### Section 2

---

#### Section 2.1

---

#### Section 2.2

***

### Section 3"""

[<Test>]
let ``can generate sections from markdown`` () =
  let slides = (md |> FsReveal.GetPresentationFromMarkdown).Slides

  slides.Length |> shouldEqual 3