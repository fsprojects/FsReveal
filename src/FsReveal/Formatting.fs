module FsReveal.Formatting

open FsReveal
open FSharp.Literate
open System.Text
open System.IO
open System

let (|SpeakerNote|OtherLine|) (line : string) =
    if line.StartsWith("' ") then SpeakerNote (line.Substring(2) + "<br/>")
    else OtherLine line

let replaceSpeakerNotes text =
    let rec loop inNotes = function
        | (SpeakerNote note) :: lines -> 
            if inNotes then note::(loop true lines)
            else "<aside class=\"notes\">"::note::(loop true lines)
        | (OtherLine line) :: lines -> 
            if inNotes then "</aside>"::line::(loop false lines)
            else line::(loop false lines)
        | _ -> []
    text |> Array.toList |> loop false

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