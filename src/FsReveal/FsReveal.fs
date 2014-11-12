namespace FsReveal

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

[<AutoOpen>]
module internal Misc =

  /// Correctly combine two paths
  let (@@) a b = Path.Combine(a, b)

  /// Ensure that directory exists
  let ensureDirectory path =
    let dir = DirectoryInfo(path)
    if not dir.Exists then dir.Create()

  /// Copy all files from source to target
  let rec copyFiles source target =
    ensureDirectory target
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

module FsRevealHelper =
  let mutable Folder = __SOURCE_DIRECTORY__

type FsReveal =
  static member GetPresentationFromScriptString fsx =
    let fsi = FsiEvaluator() 
    Literate.ParseScriptString (fsx, fsiEvaluator = fsi)
    |> getPresentation 

  static member GetPresentationFromMarkdown md =
    md 
    |> Literate.ParseMarkdownString
    |> getPresentation

  static member GenerateOutput outDir outFile presentation =
    if Directory.Exists outDir then
      printfn "%s exists.." outDir
    else
      Directory.CreateDirectory outDir |> ignore
      printfn "Create %s.." outDir

    let doc = Literate.FormatLiterateNodes presentation.Document 
  
    let htmlSlides = Literate.WriteHtml doc
    let toolTips = doc.FormattedTips

    let relative subdir = FsRevealHelper.Folder @@ subdir    
    printfn "Apply template : %s" (relative "template.html")
    let output = StringBuilder(File.ReadAllText (relative "template.html"))    

    // replace properties
    presentation.Properties
    |> List.iter (fun (k,v) -> 
      let tag = sprintf "{%s}" k    
      output.Replace(tag, v) |> ignore)

    output
      .Replace("{slides}", htmlSlides)
      .Replace("{tooltips}", toolTips) |> ignore

    File.WriteAllText (outDir @@ outFile, output.ToString())

    let revealJsDir = (FsRevealHelper.Folder @@ "../reveal.js")
    printfn "Copy reveal.js files from %s to %s" revealJsDir outDir 
    copyFiles revealJsDir outDir 

  
  static member private checkIfFileExistsAndRun file f =
    if File.Exists file then
      f()
    else
      printfn "%s does not exist. Abort!" file

  static member GenerateOutputFromScriptFile outDir outFile fsxFile =
    FsReveal.checkIfFileExistsAndRun fsxFile (fun () -> 
          let fsx = File.ReadAllText (fsxFile)

          fsx 
          |> FsReveal.GetPresentationFromScriptString
          |> FsReveal.GenerateOutput outDir outFile
      )

  static member GenerateOutputFromMarkdownFile outDir outFile mdFile =
    FsReveal.checkIfFileExistsAndRun mdFile (fun () -> 
          let md = File.ReadAllText (mdFile)

          md 
          |> FsReveal.GetPresentationFromMarkdown
          |> FsReveal.GenerateOutput outDir outFile
      )       

