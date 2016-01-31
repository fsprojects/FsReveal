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

---
- background : image2-1.png

#### Section 2.1

***

### Section 3"""

[<Test>]
let ``can read properties from slides``() = 
    let doc = md |> FsReveal.GetPresentationFromMarkdown
    match doc.Slides.[0] with
    | Simple slide ->
        let slideProperties = slide.Properties
        slideProperties.["background"] |> shouldEqual "image.png"
        slideProperties.["background-repeat"] |> shouldEqual "repeat"
    | _ -> failwith "first slide should be a simple one"

let md2 = """***
- no property
- background-repeat : repeat

### Section 1

***
- data-background : images/smalllogo.png
- data-background-repeat : repeat
- data-background-size : 100px

### Section 2

- Some bullet point

---

#### Section 2.1

***

### Section 3"""

[<Test>]
let ``can read properties from slides with list``() = 
    let doc = md2 |> FsReveal.GetPresentationFromMarkdown
    match doc.Slides.[1] with
    | Nested slides ->
        let firstNestedSlideProperties = slides.[0].Properties
        firstNestedSlideProperties.["data-background"] |> shouldEqual "images/smalllogo.png"
        firstNestedSlideProperties.["data-background-repeat"] |> shouldEqual "repeat"
        firstNestedSlideProperties.["data-background-size"] |> shouldEqual "100px"
        let secondNestedSlideProperties = slides.[1].Properties
        secondNestedSlideProperties |> shouldEqual Map.empty
    | _ -> failwith "first slide should be a nested one"

let testTemplate ="{slides}"

let normalizeLineBreaks (text:string) = text.Replace("\r\n","\n").Replace("\n","\n")

let expectedOutput = """<section >
<ul>
<li>no property</li>
<li>background-repeat : repeat</li>
</ul>
<h3>Section 1</h3>
</section>
<section >
<section data-background="images/smalllogo.png" data-background-repeat="repeat" data-background-size="100px">
<h3>Section 2</h3>
<ul>
<li>Some bullet point</li>
</ul>
</section>
<section >
<h4>Section 2.1</h4>
</section>
</section>
<section >
<h3>Section 3</h3>
</section>

"""

[<Test>]
let ``should not render slide properties``() = 
    let presentation = FsReveal.GetPresentationFromMarkdown md2
    Formatting.GenerateHTML testTemplate presentation
    |> normalizeLineBreaks
    |> shouldEqual (normalizeLineBreaks expectedOutput)
