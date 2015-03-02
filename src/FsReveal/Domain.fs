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
    { Properties : (string * string) list
      SlideData : SlideData }

type Presentation = 
    { Properties : (string * string) list
      Slides : Slide list
      Document : LiterateDocument }
