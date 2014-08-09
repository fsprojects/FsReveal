#load @"..\packages\FsReveal.0.0.4-beta\fsreveal\fsreveal.fsx"
open FsReveal

let defaultOutDir = @"c:\output"

open System.IO
let inputMd = Path.Combine( __SOURCE_DIRECTORY__, @"..\TryFsReveal\input.md")
let inputFsx = Path.Combine( __SOURCE_DIRECTORY__, @"..\TryFsReveal\input.fsx")

(*
    frc [-o outDir] (-i inputMd|-f inputFsx)
    outDir output directory
    inputMd input markdown 
    inputFsx F# script (fsx)
*)

let argvFsxAndOut = [|"-o"; defaultOutDir; "-f"; inputFsx|]
let argvMdAndOut = [|"-o"; defaultOutDir; "-i"; inputMd|]
let argvFsxInput = [|"-f"; inputFsx|]
let argvFsxMd = [|"-i"; inputFsx|]
let argvEmpty = [||]
let argvHelp = [|"-h"|]
let argvBad1 = [|"-i"|]

type CommandLineOptions = 
  {
    fsx: bool
    outDir: string
    input: string
  }

let parseArgv argv =
  let usage = """
frc [-o outDir] (-i inputMd|-f inputFsx)
outDir output directory
inputMd input markdown 
inputFsx F# script (fsx)"""

  let rec parseArgv' commandLineOptions argv = 
    match argv with
    | [] -> Some(commandLineOptions)
    | "-o"::outDir::rest ->
      parseArgv' { commandLineOptions with outDir = outDir } rest 
    | "-f"::inputFsx::rest ->
      parseArgv' { commandLineOptions with fsx = true; input = inputFsx } rest
    | "-i"::inputMd::rest ->
      parseArgv' { commandLineOptions with fsx = false; input = inputMd } rest
    | ["-h"]
    | _ -> 
      printfn "%s" usage 
      None

  parseArgv' { fsx = false; outDir = defaultOutDir; input = "input.md"} (argv|> List.ofArray)

parseArgv argvFsxAndOut
parseArgv argvMdAndOut
parseArgv argvFsxInput
parseArgv argvFsxMd
parseArgv argvEmpty
parseArgv argvHelp
parseArgv argvBad1

match (parseArgv argvFsxAndOut) with
| Some {fsx = true;outDir = outDir; input = input} -> outDir
| _ -> "???"
