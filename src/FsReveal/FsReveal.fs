module FsReveal

open System
open System.IO
open System.Collections.Generic
open System.Text
open FSharp.Literate
open FSharp.Markdown
open FSharp.Markdown.Html

type Slide =
  | Simple of MarkdownParagraph list
  | Nested of MarkdownParagraph list list

type Presentation = 
  {
    Properties: (string * string) list
    Slides: Slide list
    Document: LiterateDocument
  }

/// Split a list into chunks using the specified separator
/// This takes a list and returns a list of lists (chunks)
/// that represent individual groups, separated by the given
/// separator 'v'
let splitBy v list =
  let yieldRevNonEmpty list = 
    if list = [] then []
    else [List.rev list]

  let rec loop groupSoFar list = seq { 
    match list with
    | [] -> yield! yieldRevNonEmpty groupSoFar
    | head::tail when head = v ->
        yield! yieldRevNonEmpty groupSoFar
        yield! loop [] tail
    | head::tail ->
        yield! loop (head::groupSoFar) tail }
  loop [] list |> List.ofSeq


let getPresentation (doc:LiterateDocument) =
  /// get properties, a list of (key,value) from
  /// [[Span[Literal "key : value"]]]
  let getProperties (spans: list<list<_>>) =
    spans
    |> List.map (fun c ->       
      match c with
      | [Span(l)] -> 
        match l with
        | [Literal(v)] -> 
            let colonPos = v.IndexOf(':')
            let key = v.Substring(0, colonPos).Trim()
            let value = v.Substring(colonPos + 1).Trim()
            (key, value)
        | _ -> failwith "Invalid Presentation Property."
      | _ -> failwith "Invalid Presentation Property."
    )
  
  // main section is separated by ***
  let sections = splitBy (HorizontalRule('*')) doc.Paragraphs
  let properties = 
    match sections.Head with
    | [ListBlock(_, spans)] -> getProperties spans
    | _ -> failwith "Invalid Presentation Properties."

  let wrappedInSection paragraphs = InlineBlock("<section>")::paragraphs@[InlineBlock("</section>")]

  let getParagraphsFromSlide = function
    | Simple(paragraphs) ->
        wrappedInSection paragraphs        
    | Nested(listOfParagraphs) -> 
        listOfParagraphs         
        |> List.collect (wrappedInSection)
        |> wrappedInSection

  let slides = 
    sections.Tail
    |> List.map (fun s -> 
        // sub-section is separated by ---
        let result = splitBy (HorizontalRule('-')) s
        match result with
        | [slide] -> Simple(slide)
        | _ -> Nested(result)
      )
    
  let paragraphs = List.collect (getParagraphsFromSlide) slides

  {
    Properties = properties
    Slides = slides
    Document = doc.With(paragraphs = paragraphs)
  }

let getPresentationFromScriptString fsx =
  fsx 
  |> Literate.ParseScriptString
  |> getPresentation 

let getPresentationFromMarkdown md =
  md 
  |> Literate.ParseMarkdownString
  |> getPresentation

let generateOutput outDir outFile presentation =
  let fsi = FsiEvaluator()  
  let doc = 
    Literate.FormatAndEvaluateCodeSnippets (presentation.Document, fsi)
    |> Literate.FormatLiterateNodes
  
  let htmlSlides = Literate.WriteHtml doc
  let toolTips = doc.FormattedTips

  let relative subdir = Path.Combine(__SOURCE_DIRECTORY__, subdir)
  let output = StringBuilder(File.ReadAllText (relative "template.html"))

  // replace properties
  presentation.Properties
  |> List.iter (fun (k,v) -> 
    let tag = sprintf "{%s}" k    
    output.Replace(tag, v) |> ignore)

  output
    .Replace("{slides}", htmlSlides)
    .Replace("{tooltips}", toolTips) |> ignore

  File.WriteAllText (Path.Combine(outDir, outFile), output.ToString())