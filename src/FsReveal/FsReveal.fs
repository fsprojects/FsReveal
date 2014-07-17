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

let getPresentation paragraphs =  
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
  let sections = splitBy (HorizontalRule('*')) paragraphs
  let properties = 
    match sections.Head with
    | [ListBlock(_, spans)] -> getProperties spans
    | _ -> failwith "Invalid Presentation Properties."

  let slides = 
    sections.Tail
    |> List.map (fun s -> 
        // sub-section is separated by ---
        let result = splitBy (HorizontalRule('-')) s
        match result with
        | [slide] -> Simple(slide)
        | _ -> Nested(result)
      )

  {
    Properties = properties
    Slides = slides
  }

let getPresentationFromScriptString fsx =
  let doc = Literate.ParseScriptString(fsx)
  doc, getPresentation doc.Paragraphs

let getPresentationFromMarkdown md =
  let doc = Literate.ParseMarkdownString(md)
  doc, getPresentation doc.Paragraphs

let getParagraphsFromSlides (slides:Slide list) =
  let wrappedInSection paragraphs = InlineBlock("<section>")::paragraphs@[InlineBlock("</section>")]

  let getParagraphsFromSlide = function
    | Simple(paragraphs) ->
        wrappedInSection paragraphs        
    | Nested(listOfParagraphs) -> 
        listOfParagraphs         
        |> List.collect (wrappedInSection)
        |> wrappedInSection
        
  List.collect (getParagraphsFromSlide) slides

let getHtmlSlidesAndTooltips (doc:LiterateDocument) paragraphs =  
  let doc' = doc.With(paragraphs = paragraphs)  
    
  let doc'' = Literate.FormatLiterateNodes doc'  
  (doc''|> Literate.WriteHtml), doc''.FormattedTips


let generateOutput outDir outFile doc presentation =
  let paragraphs = getParagraphsFromSlides presentation.Slides
  let htmlSlides, toolTips = getHtmlSlidesAndTooltips doc paragraphs

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

let processMarkdownFile outDir outFile md =
  let doc, presentation = getPresentationFromMarkdown md 
  generateOutput outDir outFile doc presentation

let processScriptFile outDir outFile fsx =
  let doc, presentation = getPresentationFromScriptString fsx 
  generateOutput outDir outFile doc presentation