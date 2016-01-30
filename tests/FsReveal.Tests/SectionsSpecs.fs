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

#### Slide 2.2

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
    match slide with
    | Slide.Nested x -> ()
    | _ -> failwith "subslides not parsed"

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

[<Test>]
let ``can generate sections from markdown without properties``() = 
    let slides = (md2 |> FsReveal.GetPresentationFromMarkdown).Slides
    slides.Length |> shouldEqual 3
    let slide = slides.[1]
    match slide with
    | Slide.Nested x -> ()
    | _ -> failwith "subslides not parsed"

let normalizeLineBreaks (text:string) = text.Replace("\r\n","\n").Replace("\n","\n")


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

[<Test>]
let ``can generate html sections from markdown``() = 
    let presentation = FsReveal.GetPresentationFromMarkdown md
    Formatting.GenerateHTML testTemplate presentation
    |> normalizeLineBreaks
    |> shouldEqual (normalizeLineBreaks expectedOutput)
