module FsReveal.FsRevealIntroTest

open System.IO
open FsReveal
open Expecto
open Expecto.Flip

[<Tests>]
let tests =
  testList "intro" [
    testCase "can read FsReveal intro" <| fun () ->
      let doc = FsReveal.GenerateOutputFromMarkdownFile("Index.md", ".", "index.html")
      File.Exists "index.html"
        |> Expect.isTrue "Should have index.html"

    testCase "can create intro twice" <| fun () ->
      let doc = FsReveal.GenerateOutputFromMarkdownFile("Index.md", ".", "index.html")
      let doc = FsReveal.GenerateOutputFromMarkdownFile("Index.md", ".", "sample.html")

      File.Exists "index.html"
        |> Expect.isTrue "Should have index.html"
      File.Exists "sample.html"
        |> Expect.isTrue "Should have index.html"
  ]