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

let normalizeLineBreaks (text:string) = text.Replace("\r\n","\n").Replace("\n","\n")

/// Ensure that directory exists
let ensureDirectory path = 
    let dir = DirectoryInfo(path)
    if not dir.Exists then dir.Create()

/// Copy all files from source to target
let rec copyFiles filter source target = 
    ensureDirectory target
    if Directory.Exists(source) then 
        for f in Directory.GetDirectories(source) do
            copyFiles filter f (target @@ Path.GetFileName(f))
        for f in Directory.GetFiles(source) do
            if not <| filter f then File.Copy(f, (target @@ Path.GetFileName(f)), true)

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