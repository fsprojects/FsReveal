module FsReveal.SlidePropertySpecs

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

[<Tests>]
let tests =
  testCase "can read properties from slides" <| fun () ->
    let doc = FsReveal.GetPresentationFromMarkdown md
    match doc.Slides.[0] with
    | Simple slide ->
      let slideProperties = slide.Properties
      slideProperties.["background"] |> Expect.equal "Background is the right image" "image.png"
      slideProperties.["background-repeat"] |> Expect.equal "Background-repeat is correct" "repeat"
    | _ ->
      failwith "first slide should be a simple one"

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

[<Tests>]
let tests2 =
  testCase "can read properties from slides with list" <| fun () ->
    let doc = FsReveal.GetPresentationFromMarkdown md2
    let slides =
      match doc.Slides.[1] with
      | Nested slides -> slides
      | _ -> failwith "first slide should be a nested one"

    let firstNestedSlideProperties = slides.[0].Properties
    firstNestedSlideProperties.["data-background"]
      |> Expect.equal "Can set data properties" "images/smalllogo.png"

    firstNestedSlideProperties.["data-background-repeat"]
      |> Expect.equal "Can set data properties 2" "repeat"

    firstNestedSlideProperties.["data-background-size"]
      |> Expect.equal "Can set data properties 3" "100px"

    let secondNestedSlideProperties = slides.[1].Properties
    secondNestedSlideProperties
      |> Expect.equal "Has no properties for the second slide" Map.empty


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

[<Tests>]
let tests3 =
  testCase "should not render slide properties" <| fun () ->
    let presentation = FsReveal.GetPresentationFromMarkdown md2
    Formatting.GenerateHTML testTemplate presentation
    |> normalizeLineBreaks
    |> Expect.equal "Normalised slides" (normalizeLineBreaks expectedOutput)