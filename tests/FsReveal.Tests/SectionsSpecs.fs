module FsReveal.SectionsSpecs

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

### Slide 1

***

### Slide 2

---

#### Slide 2.1

---

#### Slide 2.2

***

### Slide 3"""

[<Tests>]
let tests =
  testCase "can generate sections from markdown" <| fun () ->
    let presentation = FsReveal.GetPresentationFromMarkdown md
    let slides = presentation.Slides

    slides.Length |> Expect.equal "Should have three slides" 3

    let slide =
      slides
      |> Seq.skip 1
      |> Seq.head

    match slide with
    | Slide.Nested x ->
      ()
    | _ ->
      failwith "subslides not parsed"

let md2 = """

***

### Slide 1

***

### Slide 2

---

#### Slide 2.1

---

#### Slide 2.2

***

### Slide 3"""

[<Tests>]
let markdowns =
  testCase "can generate sections from markdown without properties" <| fun () ->
    let presentation = FsReveal.GetPresentationFromMarkdown md2
    let slides = presentation.Slides
    slides.Length |> Expect.equal "Should have three slides" 3
    match slides.[1] with
    | Slide.Nested x ->
      ()
    | _ ->
      failwith "subslides not parsed"

let normalizeLineBreaks (text: string) =
  text.Replace("\r\n","\n")
      .Replace("\n","\n")


let testTemplate ="{slides}"


let expectedOutput = """<section >
<h3>Slide 1</h3>
</section>
<section >
<section >
<h3>Slide 2</h3>
</section>
<section >
<h4>Slide 2.1</h4>
</section>
<section >
<h4>Slide 2.2</h4>
</section>
</section>
<section >
<h3>Slide 3</h3>
</section>

"""

[<Tests>]
let outputs =
  testCase "can generate html sections from markdown" <| fun () ->
    let presentation = FsReveal.GetPresentationFromMarkdown md
    Formatting.GenerateHTML testTemplate presentation
    |> normalizeLineBreaks
    |> Expect.equal "Output with linebreaks equals" (normalizeLineBreaks expectedOutput)
