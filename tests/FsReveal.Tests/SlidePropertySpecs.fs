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

***

### Section 3"""

[<Test>]
let ``can read properties from slides``() = 
    let doc = md |> FsReveal.GetPresentationFromMarkdown
    let slideProperties = doc.Slides.[0].Properties
    slideProperties.["background"] |> shouldEqual "image.png"
    slideProperties.["background-repeat"] |> shouldEqual "repeat"

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

***

### Section 3"""

[<Test>]
let ``can read properties from slides with list``() = 
    let doc = md2 |> FsReveal.GetPresentationFromMarkdown
    doc.Slides.[0].Properties |> shouldBeEmpty
    let slideProperties = doc.Slides.[1].Properties
    slideProperties.["data-background"] |> shouldEqual "images/smalllogo.png"
    slideProperties.["data-background-repeat"] |> shouldEqual "repeat"
    slideProperties.["data-background-size"] |> shouldEqual "100px"


let testTemplate ="{slides}"

let normalizeLineBreaks (text:string) = text.Replace("\r\n","\n").Replace("\n","\n")

let expectedOutput = """<section >
<ul>
<li>no property</li>
<li>background-repeat : repeat</li>
</ul>
<h3>Section 1</h3>
</section>
<section data-background="images/smalllogo.png" data-background-repeat="repeat" data-background-size="100px">
<h3>Section 2</h3>
<ul>
<li>Some bullet point</li>
</ul>
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
