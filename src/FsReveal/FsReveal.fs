namespace FsReveal

open System
open System.IO
open System.Collections.Generic
open System.Text
open FSharp.Literate
open FSharp.Markdown
open FSharp.Markdown.Html

module FsRevealHelper = 
    let mutable Folder = __SOURCE_DIRECTORY__

type FsReveal = 
    
    static member GetPresentationFromScriptString fsx = 
        let fsi = FsiEvaluator()
        Literate.ParseScriptString(fsx, fsiEvaluator = fsi) |> getPresentation
    
    static member GetPresentationFromMarkdown md = 
        md
        |> Literate.ParseMarkdownString
        |> getPresentation
    
    static member GenerateOutput outDir outFile presentation = 
        if Directory.Exists outDir then printfn "%s exists.." outDir
        else 
            Directory.CreateDirectory outDir |> ignore
            printfn "Create %s.." outDir
        let revealJsDir = FsRevealHelper.Folder @@ "../reveal.js"
        printfn "Copy reveal.js files from %s to %s" revealJsDir outDir
        copyFiles revealJsDir outDir
        // delete overhead
        File.Delete(outDir @@ "index.html")
        File.Delete(outDir @@ "README.md")
        let di = DirectoryInfo(outDir @@ "test")
        di.Delete(true)
        let doc = Literate.FormatLiterateNodes presentation.Document
        let htmlSlides = Literate.WriteHtml doc
        let toolTips = doc.FormattedTips
        let relative subdir = FsRevealHelper.Folder @@ subdir
        printfn "Apply template : %s" (relative "template.html")
        let output = StringBuilder(File.ReadAllText(relative "template.html"))
        // replace properties
        presentation.Properties |> List.iter (fun (k, v) -> output.Replace(sprintf "{%s}" k, v) |> ignore)
        output.Replace("{slides}", htmlSlides).Replace("{tooltips}", toolTips) |> ignore
        File.WriteAllText(outDir @@ outFile, output.ToString())
    
    static member private checkIfFileExistsAndRun file f = 
        if File.Exists file then f()
        else printfn "%s does not exist. Abort!" file
    
    static member GenerateOutputFromScriptFile outDir outFile fsxFile = 
        FsReveal.checkIfFileExistsAndRun fsxFile (fun () -> 
            fsxFile
            |> File.ReadAllText
            |> FsReveal.GetPresentationFromScriptString
            |> FsReveal.GenerateOutput outDir outFile)
    
    static member GenerateOutputFromMarkdownFile outDir outFile mdFile = 
        FsReveal.checkIfFileExistsAndRun mdFile (fun () -> 
            mdFile
            |> File.ReadAllText
            |> FsReveal.GetPresentationFromMarkdown
            |> FsReveal.GenerateOutput outDir outFile)
