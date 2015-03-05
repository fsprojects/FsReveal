module FsReveal.PropertySpecs

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
let ``can read properties from markdown``() = 
    let properties = (md |> FsReveal.GetPresentationFromMarkdown).Properties
    properties.["title"] |> shouldEqual "FsReveal"
    properties.["description"] |> shouldEqual "Introduction to FsReveal"
    properties.["theme"] |> shouldEqual "Night"
    properties.["transition"] |> shouldEqual "default"

let defaultMD = """
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
let ``uses default properties if nothing is specified in markdown``() = 
    let properties = (defaultMD |> FsReveal.GetPresentationFromMarkdown).Properties
    properties.["title"] |> shouldEqual "Presentation"
    properties.["description"] |> shouldEqual ""
    properties.["theme"] |> shouldEqual "night"
    properties.["transition"] |> shouldEqual "default"