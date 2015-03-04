module FsReveal.Formatting

open FsReveal
open FSharp.Literate
open System.Text
open System.IO
open System

let replaceSpeakerNotes (text : string []) = 
    text |> Array.map (fun line -> 
                if line.StartsWith("' ") then "<aside class=\"notes\">" + line.Substring(2) + "</aside>"
                else line)

let preprocessing (text : string []) = 
    text 
    |> replaceSpeakerNotes
    |> fun s -> String.Join(Environment.NewLine,s)
    
/// Generates a HTML page from a presentation
let GenerateHTML (template:string) presentation =
    let doc = Literate.FormatLiterateNodes presentation.Document
    let htmlSlides = Literate.WriteHtml doc
    let toolTips = doc.FormattedTips    
    let output = StringBuilder(template)
    // replace properties
    presentation.Properties |> List.iter (fun (k, v) -> output.Replace(sprintf "{%s}" k, v) |> ignore)
    output.Replace("{slides}", htmlSlides).Replace("{tooltips}", toolTips) |> ignore
    output.ToString()