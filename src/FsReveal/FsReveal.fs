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
    let mutable StyleFile = Path.Combine(__SOURCE_DIRECTORY__,"fsreveal.css")

type FsReveal private() = 
    static let defaultFileName (other:FileInfo) optInput = 
        match optInput with 
        | Some fn -> fn
        | None -> other.Name.Replace(other.Extension, ".html")

    static let generateOutput outDir outFile presentation = 
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
        let cssDir = outDir @@ "css" 
        printfn "Copy fsreveal.css style from %s to %s" FsRevealHelper.StyleFile cssDir
        if Directory.Exists cssDir |> not then 
            Directory.CreateDirectory cssDir |> ignore
            printfn "Creating %s.." cssDir

        File.Copy(FsRevealHelper.StyleFile, cssDir @@ "fsreveal.css", true)

    static let getPresentationFromScriptLines fsxFile fsiEvaluator lines = 
        let fsx = Formatting.preprocessing lines
        Literate.ParseScriptString(fsx, ?path=Option.map Path.GetFullPath fsxFile, ?fsiEvaluator = fsiEvaluator) 
        |> getPresentation
    
    static let getPresentationFromMarkdownLines mdFile fsiEvaluator lines = 
        let md = Formatting.preprocessing lines
        Literate.ParseMarkdownString(md, ?path=Option.map Path.GetFullPath mdFile, ?fsiEvaluator = fsiEvaluator) 
        |> getPresentation

    static let checkIfFileExistsAndRun file f = 
        if File.Exists file then f()
        else printfn "%s does not exist. Abort!" file
          
    /// Creates a presentation from an F# Script file specified as string. This also evaluates
    /// all F# code snippets in the presentation and embeds the outputs. If you do not specify
    /// a custom FSI evaluator, a new default one is created. See `GenerateFromFile` for more info.
    static member GetPresentationFromScriptString(text : string, ?fsxFile, ?fsiEvaluator) = 
        normalizeLineBreaks(text).Split('\n')
        |> getPresentationFromScriptLines fsxFile fsiEvaluator

    /// Creates a presentation from a Markdown source file specified as string. This also evaluates
    /// all F# code snippets in the presentation and embeds the outputs. If you do not specify
    /// a custom FSI evaluator, a new default one is created. See `GenerateFromFile` for more info.
    static member GetPresentationFromMarkdown(text : string, ?mdFile, ?fsiEvaluator) = 
        normalizeLineBreaks(text).Split('\n')
        |> getPresentationFromMarkdownLines mdFile fsiEvaluator
    
    /// Write the specified presentation to a specified file in the output directory.
    /// (if a file name is not specified, the default `index.html` will be used)
    static member GenerateOutput(presentation, outDir, ?outFile) = 
        generateOutput outDir (defaultArg outFile "index.html") presentation
            
    /// Processes a presentation specified as an F# Script. This also evaluates all 
    /// F# code snippets in the presentation and embeds the outputs. If you do not specify
    /// a custom FSI evaluator, a new default one is created. See `GenerateFromFile` for more info.
    static member GenerateOutputFromScriptFile(fsxFile, outDir, ?outFile, ?fsiEvaluator) = 
        checkIfFileExistsAndRun fsxFile (fun () -> 
            fsxFile
            |> File.ReadAllLines
            |> getPresentationFromScriptLines (Some fsxFile) fsiEvaluator
            |> generateOutput outDir (defaultFileName (FileInfo fsxFile) outFile))
    
    /// Processes a presentation specified as a Markdown. This also evaluates all 
    /// F# code snippets in the presentation and embeds the outputs. If you do not specify
    /// a custom FSI evaluator, a new default one is created. See `GenerateFromFile` for more info.
    static member GenerateOutputFromMarkdownFile(mdFile, outDir, ?outFile, ?fsiEvaluator) = 
        checkIfFileExistsAndRun mdFile (fun () -> 
            mdFile
            |> File.ReadAllLines
            |> getPresentationFromMarkdownLines  (Some mdFile) fsiEvaluator 
            |> generateOutput outDir (defaultFileName (FileInfo mdFile) outFile))
    
    /// Processes a presentation specified as an F# Script file or a Markdown document. 
    /// (When the file name has extension other than `fsx` or `md`, nothing happens).
    ///
    /// The method evaluates F# code snippets in the presentation and embeds the outputs. You
    /// can specify a custom `fsiEvaluator` to add formatting for custom values and handle errors
    /// during the valuation.
    ///
    /// ## Parameters
    /// - `fsiEvaluator` - Custom evaluator (you can use this parameter to add custom formatting
    ///   that turns values into HTML when embedding them into the presentation)
    static member GenerateFromFile(fileName, outDir, ?outFile, ?fsiEvaluator) = 
        let file = FileInfo fileName
        let outputFileName = defaultFileName file outFile
        match file.Extension with
        | ".md" -> FsReveal.GenerateOutputFromMarkdownFile(file.FullName, outDir, outputFileName, ?fsiEvaluator=fsiEvaluator)
        | ".fsx" -> FsReveal.GenerateOutputFromScriptFile(file.FullName, outDir, outputFileName, ?fsiEvaluator=fsiEvaluator)
        | _ -> ()
