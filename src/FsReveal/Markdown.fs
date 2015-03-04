[<AutoOpen>]
module internal FsReveal.Markdown

open System
open System.IO
open System.Collections.Generic
open System.Text
open FSharp.Literate
open FSharp.Markdown
open FSharp.Markdown.Html

let getPresentation (doc : LiterateDocument) = 
    /// get properties, a list of (key,value) from
    /// [[Span[Literal "key : value"]]]
    let getProperties (spans : list<list<_>>) = 
        let extractProperty paragraphs = 
            match paragraphs with
            | [ Span(l) ] -> 
                match l with
                | [ Literal(v) ] when v.Contains(":") -> 
                    let colonPos = v.IndexOf(':')
                    let key = v.Substring(0, colonPos).Trim()
                    let value = v.Substring(colonPos + 1).Trim()
                    (key, value)
                | _ -> failwithf "Invalid Presentation property: %A" l
            | _ -> failwithf "Invalid Presentation property: %A" paragraphs
        spans |> List.map extractProperty
    
    // main section is separated by ***
    let sections = splitBy (HorizontalRule('*')) doc.Paragraphs
    
    let properties = 
        match sections.Head with
        | [ ListBlock(_, spans) ] -> getProperties spans
        | x -> failwithf "Missing Presentation properties in: %A" x
    
    let wrappedInSection properties paragraphs = 
        let attributes = properties |> Seq.map (fun (k, v) -> sprintf "%s=\"%s\"" k v)
        InlineBlock(sprintf "<section %s>" (String.Join(" ", attributes))) :: paragraphs @ [ InlineBlock("</section>") ]
    
    let getParagraphsFromSlide slide = 
        match slide.SlideData with
        | Simple(paragraphs) -> wrappedInSection slide.Properties paragraphs
        | Nested(listOfParagraphs) -> 
            listOfParagraphs
            |> List.collect (wrappedInSection slide.Properties)
            |> wrappedInSection slide.Properties
    
    let extractSlide paragraphs = 
        // sub-section is separated by ---
        let result = splitBy (HorizontalRule('-')) paragraphs
        
        let properties, data = 
            match paragraphs with
            | ListBlock(_, spans) :: data -> 
                try 
                    getProperties spans, [ data ]
                with _ -> [], [ paragraphs ]
            | _ -> [], [ paragraphs ]
        { Properties = properties
          SlideData = 
              match data with
              | [ [ slide ] ] -> Simple([ slide ])
              | _ -> Nested(result) }
    
    let slides = sections.Tail |> List.map extractSlide
    let paragraphs = List.collect getParagraphsFromSlide slides
    { Properties = properties
      Slides = slides
      Document = doc.With(paragraphs = paragraphs) }
