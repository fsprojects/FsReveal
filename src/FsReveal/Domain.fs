namespace FsReveal

open System
open System.IO
open System.Collections.Generic
open System.Text
open FSharp.Literate
open FSharp.Markdown
open FSharp.Markdown.Html

type SlideData = 
    | Simple of MarkdownParagraph list
    | Nested of MarkdownParagraph list list

type Slide = 
    { Properties : Map<string,string>
      SlideData : SlideData }

type Presentation = 
    { Properties : Map<string,string>
      Slides : Slide list
      Document : LiterateDocument }
