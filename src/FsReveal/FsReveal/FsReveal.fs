namespace FsReveal

open FSharp.Literate
open FSharp.Markdown
open FSharp.CodeFormat
open System.Collections.Generic
open CSharpFormat
open System
open System.IO
open System.Web
open System.Text.RegularExpressions
open System.Text


type FsReveal() =
  static let formattingContext templateFile format prefix lineNumbers includeSource replacements layoutRoots =
    { TemplateFile = templateFile 
      Replacements = defaultArg replacements []
      GenerateLineNumbers = defaultArg lineNumbers true
      IncludeSource = defaultArg includeSource false
      Prefix = defaultArg prefix "fs"
      OutputKind = defaultArg format OutputKind.Html
      LayoutRoots = defaultArg layoutRoots [] }

  static let rec collectCodes par = seq {
    match par with 
    | Matching.LiterateParagraph(HiddenCode(Some id, lines)) -> 
        yield Choice2Of2(id), lines
    | Matching.LiterateParagraph(DoNotEvalCode(lines))
    | Matching.LiterateParagraph(NamedCode(_,lines))
    | Matching.LiterateParagraph(FormattedCode(lines)) ->         
        yield Choice1Of2(lines), lines
    | Matching.ParagraphNested(pn, nested) ->
        yield! Seq.collect (Seq.collect collectCodes) nested
    | _ -> () }

  static let rec replaceSpecialCodes ctx (formatted:IDictionary<_, _>) = function
    | Matching.LiterateParagraph(special) -> 
      match special with
      | HiddenCode _ -> None
      | CodeReference ref -> Some (formatted.[Choice2Of2 ref])
      | OutputReference _  
      | ItValueReference _  
      | ValueReference _ -> 
          failwith "Output, it-value and value references should be replaced by FSI evaluator"
      | DoNotEvalCode lines
      | NamedCode(_,lines)
      | FormattedCode lines -> Some (formatted.[Choice1Of2 lines])      
      | LanguageTaggedCode(lang, code) -> 
        let inlined = 
          match ctx.OutputKind with
          | OutputKind.Html ->
              let code = HttpUtility.HtmlEncode code
              let code = SyntaxHighlighter.FormatCode(lang, code)
              sprintf "<pre lang=\"%s\">%s</pre>" lang code
          | OutputKind.Latex ->
              sprintf "\\begin{lstlisting}\n%s\n\\end{lstlisting}" code
        Some(InlineBlock(inlined))
      | StartSlide -> Some(StartSlideBlock)
      | EndSlide -> Some(EndSlideBlock)
    // Traverse all other structures recursively
    | Matching.ParagraphNested(pn, nested) ->
        let nested = List.map (List.choose (replaceSpecialCodes ctx formatted)) nested
        Some(Matching.ParagraphNested(pn, nested))
    | par -> Some par

  static let replaceLiterateParagraphs ctx (doc:LiterateDocument) = 
    let replacements = Seq.collect collectCodes doc.Paragraphs
    let snippets = [| for _, r in replacements -> Snippet("", r) |]    
    
    // Format all snippets and build lookup dictionary for replacements
    let formatted = CodeFormat.FormatHtml(snippets, ctx.Prefix, ctx.GenerateLineNumbers, false)
    
    let lookup = 
      [ for (key, _), fmtd in Seq.zip replacements formatted.Snippets -> 
          key, InlineBlock(fmtd.Content) ] |> dict 
   
    
    // Replace original snippets with formatted HTML and return document
    let newParagraphs = List.choose (replaceSpecialCodes ctx lookup) doc.Paragraphs
    doc.With(paragraphs = newParagraphs, formattedTips = formatted.ToolTip)

  static let processConfigs (fsx:string) =
    let regex =     
      Regex("^\(\*\@(?<name>[^=]*)=(?<value>[^\@]*)\@\*\)$", RegexOptions.Singleline)

    let lines = fsx.Split([|'\r';'\n'|], StringSplitOptions.RemoveEmptyEntries)

    let configs, processedLines =  
      lines
      |> List.ofArray
      |> List.fold (fun acc l -> 
        let configs, processedLines = acc
        let m = regex.Match l
        if m.Success then
          let name = m.Groups.["name"].Value.Trim()
          let value = m.Groups.["value"].Value.Trim()
          ((name,value)::configs, processedLines)
        else
          (configs, l::processedLines)
      ) ([],[])

    (configs, (processedLines |> List.fold (fun acc l -> l + "\r\n" + acc) "") )

  static member ProcessScriptFile outDir outFile fsx =
    let configs, fsx = processConfigs fsx

    let fsi = Formatters.createFsiEvaluator  "." outDir

    let slides,tooltips =FsReveal.GetHtmlWithoutFormattedTips(fsx, fsi, "FsReveal", false)
    let relative subdir = Path.Combine(__SOURCE_DIRECTORY__, subdir)
    let output = StringBuilder(File.ReadAllText (relative "template.html"))
    
    output
      .Replace("{tooltips}", tooltips)
      .Replace("{slides}", slides) |> ignore
    
    configs
    |> List.iter (fun (k,v) -> 
        let tag = sprintf "{%s}" k
        output.Replace(tag, v) |> ignore
      )

    File.WriteAllText (Path.Combine(outDir, outFile), output.ToString())

  static member GetHtmlWithoutFormattedTips(fsx, fsi, ?prefix, ?lineNumbers) =    
    let doc = Literate.ParseScriptString(fsx, fsiEvaluator = fsi)

    let ctx = formattingContext None (Some OutputKind.Html) prefix lineNumbers None None None
    let doc = replaceLiterateParagraphs ctx doc
    Markdown.WriteHtml(MarkdownDocument(doc.Paragraphs, doc.DefinedLinks)), doc.FormattedTips