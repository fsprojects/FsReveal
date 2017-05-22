#load "../../FSharp.Formatting/FSharp.Formatting.fsx"
#I "."
#r "FsReveal.dll"
open System.IO

// Workaround for https://github.com/fsharp/FSharp.Compiler.Service/issues/199
// (The file needs to be in the same place from which FSharp.Compiler.Service.dll is loaded)
printfn "copy FSharp.Compiler.Interactive.Settings ..."
let targetFCIS = __SOURCE_DIRECTORY__ + "/../../FAKE/tools/FSharp.Compiler.Interactive.Settings.dll"

if not (File.Exists (targetFCIS)) then
  System.IO.File.Copy(__SOURCE_DIRECTORY__ + "/../lib/net461/FSharp.Compiler.Interactive.Settings.dll", targetFCIS)

FsReveal.FsRevealHelper.DistFolder <- System.IO.Path.Combine(__SOURCE_DIRECTORY__)
printfn "Set dist to %s" FsReveal.FsRevealHelper.DistFolder

FsReveal.FsRevealHelper.TemplateFile <- System.IO.Path.Combine(__SOURCE_DIRECTORY__,"index.html")
FsReveal.FsRevealHelper.StyleFile <- System.IO.Path.Combine(__SOURCE_DIRECTORY__,"fsreveal.css")