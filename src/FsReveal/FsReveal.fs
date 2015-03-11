namespace FsReveal

open System
open System.IO
open System.Collections.Generic
open System.Text
open FSharp.Literate
open FSharp.Markdown
open FSharp.Markdown.Html

module FsRevealHelper = 
    // used to change the working directory
    let mutable RevealJsFolder = __SOURCE_DIRECTORY__    
    let mutable TemplateFile = Path.Combine(__SOURCE_DIRECTORY__,"template.html")

type FsReveal = 
    static member GetPresentationFromScriptString(text : string) = 
        normalizeLineBreaks(text).Split('\n') |> FsReveal.GetPresentationFromScriptLines
    static member GetPresentationFromMarkdown(text : string) = 
        normalizeLineBreaks(text).Split('\n') |> FsReveal.GetPresentationFromMarkdownLines
    
    static member GetPresentationFromScriptLines lines = 
        let fsx = Formatting.preprocessing lines
        let fsi = FsiEvaluator()
        Literate.ParseScriptString(fsx, fsiEvaluator = fsi) |> getPresentation
    
    static member GetPresentationFromMarkdownLines lines = 
        Formatting.preprocessing lines
        |> Literate.ParseMarkdownString
        |> getPresentation
    
    static member GenerateOutput outDir outFile presentation = 
        if Directory.Exists outDir |> not then 
            Directory.CreateDirectory outDir |> ignore
            printfn "Creating %s.." outDir
        printfn "Copy reveal.js files from %s to %s" FsRevealHelper.RevealJsFolder outDir
        copyFiles (fun f -> f.ToLower().Contains("index.html")) FsRevealHelper.RevealJsFolder outDir
        // delete overhead
        File.Delete(outDir @@ "README.md")
        let di = DirectoryInfo(outDir @@ "test")
        if di.Exists then di.Delete(true)
        let template = File.ReadAllText(FsRevealHelper.TemplateFile)
        printfn "Apply template : %s" FsRevealHelper.TemplateFile
        let output = Formatting.GenerateHTML template presentation
        File.WriteAllText(outDir @@ outFile, output)
    
    static member private checkIfFileExistsAndRun file f = 
        if File.Exists file then f()
        else printfn "%s does not exist. Abort!" file
    
    static member GenerateOutputFromScriptFile outDir outFile fsxFile = 
        FsReveal.checkIfFileExistsAndRun fsxFile (fun () -> 
            fsxFile
            |> File.ReadAllLines
            |> FsReveal.GetPresentationFromScriptLines
            |> FsReveal.GenerateOutput outDir outFile)
    
    static member GenerateOutputFromMarkdownFile outDir outFile mdFile = 
        FsReveal.checkIfFileExistsAndRun mdFile (fun () -> 
            mdFile
            |> File.ReadAllLines
            |> FsReveal.GetPresentationFromMarkdownLines
            |> FsReveal.GenerateOutput outDir outFile)
    
    static member GenerateFromFile outDir fileName = 
        let file = FileInfo fileName
        let outputFileName = file.Name.Replace(file.Extension, ".html")
        match file.Extension with
        | ".md" -> FsReveal.GenerateOutputFromMarkdownFile outDir outputFileName file.FullName
        | ".fsx" -> FsReveal.GenerateOutputFromScriptFile outDir outputFileName file.FullName
        | _ -> ()
