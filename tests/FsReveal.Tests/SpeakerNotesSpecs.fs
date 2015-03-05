module FsReveal.SpeakerNotesSpecs

open FsReveal
open NUnit.Framework
open FsUnit

let testTemplate ="{slides}"

let normalizeLineBreaks (text:string) = text.Replace("\r\n","\n").Replace("\n","\n")

let md = """
- title : FsReveal

***

### Slide 1

Normal Text

' Oh hey, these are some notes.
' And some more


***

### Slide 2
Normal Text
"""

let expectedOutput = """<section >

<section >

<h3>Slide 1</h3>

<p>Normal Text</p>

<aside class="notes">
Oh hey, these are some notes.<br/>
And some more<br/>
</aside>

</section>

</section>

<section >

<section >

<h3>Slide 2</h3>

<p>Normal Text</p>

</section>

</section>


"""

[<Test>]
let ``can generate sections from markdown``() = 
    let presentation = FsReveal.GetPresentationFromMarkdown md
    Formatting.GenerateHTML testTemplate presentation
    |> normalizeLineBreaks
    |> shouldEqual (normalizeLineBreaks expectedOutput)
