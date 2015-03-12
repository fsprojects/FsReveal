module FsReveal.FsRevealIntroTest

open FsReveal
open NUnit.Framework
open FsUnit

[<Test>]
let ``can read FsReveal intro``() = 
    let doc = FsReveal.GenerateOutputFromMarkdownFile "." "index.html" "Index.md" 

    System.IO.File.Exists "index.html" |> shouldEqual true


[<Test>]
let ``can create intro twice``() = 
    let doc = FsReveal.GenerateOutputFromMarkdownFile "." "index.html" "Index.md" 
    let doc = FsReveal.GenerateOutputFromMarkdownFile "." "sample.html" "Index.md" 

    System.IO.File.Exists "index.html" |> shouldEqual true
    System.IO.File.Exists "sample.html" |> shouldEqual true