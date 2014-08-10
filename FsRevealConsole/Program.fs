open FsReveal
open System.IO

type CommandLineOptions = 
  {
    fsx: bool
    outDir: string
    input: string
  }

[<EntryPoint>]
let main argv =   
  FsRevealHelper.Folder <- @".\fsreveal"

  let defaultOutDir = @"c:\output"
  let usage = """
  frc [-o outDir] (-i inputMd|-f inputFsx)
  outDir output directory
  inputMd input markdown 
  inputFsx F# script (fsx)"""

  let parseArgv argv =
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
      | ["-help"]
      | _ -> None

    parseArgv' { fsx = false; outDir = defaultOutDir; input = "input.md"} (argv|> List.ofArray)

  let commandLineOptions =  if argv.Length > 0 then parseArgv argv else None

  match commandLineOptions with
  | Some {fsx = true;outDir = outDir; input = input} ->
      printfn "FsRevealConsole does not currently support fsx."
  | Some {fsx = false;outDir = outDir; input = input} ->
      FsReveal.GenerateOutputFromMarkdownFile outDir "index.html" input
  | _ -> 
      printfn "%s" usage
    
  0 // return an integer exit code
