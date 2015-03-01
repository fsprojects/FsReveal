[<AutoOpen>]
module internal FsReveal.Misc

open System
open System.IO
open System.Collections.Generic
open System.Text
open FSharp.Literate
open FSharp.Markdown
open FSharp.Markdown.Html

/// Correctly combine two paths
let (@@) a b = Path.Combine(a, b)

/// Ensure that directory exists
let ensureDirectory path = 
    let dir = DirectoryInfo(path)
    if not dir.Exists then dir.Create()

/// Copy all files from source to target
let rec copyFiles source target = 
    ensureDirectory target
    if Directory.Exists(source) then
        for f in Directory.GetDirectories(source) do
            copyFiles f (target @@ Path.GetFileName(f))
        for f in Directory.GetFiles(source) do
            File.Copy(f, (target @@ Path.GetFileName(f)), true)

/// Split a list into chunks using the specified separator
/// This takes a list and returns a list of lists (chunks)
/// that represent individual groups, separated by the given
/// separator 'v'
let splitBy v list = 
    let yieldRevNonEmpty list = 
        if list = [] then []
        else [ List.rev list ]
    
    let rec loop groupSoFar list = 
        seq { 
            match list with
            | [] -> yield! yieldRevNonEmpty groupSoFar
            | head :: tail when head = v -> 
                yield! yieldRevNonEmpty groupSoFar
                yield! loop [] tail
            | head :: tail -> yield! loop (head :: groupSoFar) tail
        }
    
    loop [] list |> List.ofSeq

let getPresentation (doc : LiterateDocument) = 
    /// get properties, a list of (key,value) from
    /// [[Span[Literal "key : value"]]]
    let getProperties (spans : list<list<_>>) = 
        spans |> List.map (fun c -> 
                     match c with
                     | [ Span(l) ] -> 
                         match l with
                         | [ Literal(v) ] when v.Contains(":") -> 
                             let colonPos = v.IndexOf(':')
                             let key = v.Substring(0, colonPos).Trim()
                             let value = v.Substring(colonPos + 1).Trim()
                             (key, value)
                         | _ -> failwithf "Invalid Presentation property: %A" l
                     | _ -> failwithf "Invalid Presentation property: %A" c)
    
    // main section is separated by ***
    let sections = splitBy (HorizontalRule('*')) doc.Paragraphs
    
    let properties = 
        match sections.Head with
        | [ ListBlock(_, spans) ] -> getProperties spans
        | x -> failwithf "Invalid Presentation properties: %A" x
    
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
    
    let slides = 
        sections.Tail |> List.map (fun s -> 
                             // sub-section is separated by ---
                             let result = splitBy (HorizontalRule('-')) s
                             
                             let properties, data = 
                                 match s with
                                 | ListBlock(_, spans) :: data -> 
                                     try 
                                         getProperties spans, [ data ]
                                     with _ -> [], [ s ]
                                 | _ -> [], [ s ]
                             { Properties = properties
                               SlideData = 
                                   match data with
                                   | [ [ slide ] ] -> Simple([ slide ])
                                   | _ -> Nested(result) })
    
    let paragraphs = List.collect (getParagraphsFromSlide) slides
    { Properties = properties
      Slides = slides
      Document = doc.With(paragraphs = paragraphs) }
