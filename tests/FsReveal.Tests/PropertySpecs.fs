module FsReveal.PropertySpecs

open FsReveal
open Expecto
open Expecto.Flip

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

[<Tests>]
let tests =
  testCase "can read properties from markdown" <| fun () ->
    let properties = (md |> FsReveal.GetPresentationFromMarkdown).Properties
    properties.["title"]
      |> Expect.equal "Should have the correct title" "FsReveal"

    properties.["description"]
      |> Expect.equal "Should have the correct description" "Introduction to FsReveal"

    properties.["theme"]
      |> Expect.equal "Should have the correct themse" "Night"

    properties.["transition"]
      |> Expect.equal "Should have the right transition method" "default"

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

[<Tests>]
let tests2 =
  testCase "uses default properties if nothing is specified in markdown" <| fun () ->
    let presentation = defaultMD |> FsReveal.GetPresentationFromMarkdown
    let properties = presentation.Properties
    properties.["title"]
      |> Expect.equal "Presentation title" "Presentation"

    properties.["description"]
      |> Expect.stringHasLength "Presentation description" 0

    properties.["theme"]
      |> Expect.equal "Theme is correct" "night"

    properties.["transition"]
      |> Expect.equal "Transition is default" "default"