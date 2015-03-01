module FsReveal.FsRevealIntroTest

open FsReveal
open NUnit.Framework
open FsUnit

[<Test>]
let ``can read FsReveal intro``() = 
    let doc = FsReveal.GenerateOutputFromMarkdownFile "." "intro.html" "Intro.md" 

    System.IO.File.Exists "intro.html" |> shouldEqual true