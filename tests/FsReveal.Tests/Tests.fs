module FsReveal.Tests

open FsReveal
open NUnit.Framework

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
let ``can read properties from markdown`` () =
  let properties = (md |> FsReveal.getPresentationFromMarkdown).Properties

  Assert.AreEqual(("title", "FsReveal"), properties |> Seq.find (fun (k,_) -> k = "title"))
  Assert.AreEqual(("description", "Introduction to FsReveal"), properties |> Seq.find (fun (k,_) -> k = "description"))
  Assert.AreEqual(("theme", "Night"), properties |> Seq.find (fun (k,_) -> k = "theme"))
  Assert.AreEqual(("transition", "default"), properties |> Seq.find (fun (k,_) -> k = "transition"))

[<Test>]
let ``can generate sections from markdown`` () =
  let slides = (md |> FsReveal.getPresentationFromMarkdown).Slides

  Assert.AreEqual(3, slides.Length)


  
  
  

