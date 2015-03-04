module FsReveal.PropertySpecs

open FsReveal
open NUnit.Framework
open FsUnit

let emptyMD = """
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
let ``can show error on empty properties``() = 
    shouldFail(fun () -> emptyMD |> FsReveal.GetPresentationFromMarkdown |> ignore)

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
let ``can read properties from markdown``() = 
    let properties = (md |> FsReveal.GetPresentationFromMarkdown).Properties
    properties
    |> Seq.find (fun (k, _) -> k = "title")
    |> shouldEqual ("title", "FsReveal")
    properties
    |> Seq.find (fun (k, _) -> k = "description")
    |> shouldEqual ("description", "Introduction to FsReveal")
    properties
    |> Seq.find (fun (k, _) -> k = "theme")
    |> shouldEqual ("theme", "Night")
    properties
    |> Seq.find (fun (k, _) -> k = "transition")
    |> shouldEqual ("transition", "default")
