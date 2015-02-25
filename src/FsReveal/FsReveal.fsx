#load "../../FSharp.Formatting/FSharp.Formatting.fsx"
#I "../lib/net40"
#r "FsReveal.dll"

// Workaround for https://github.com/fsharp/FSharp.Compiler.Service/issues/199
// (The file needs to be in the same place from which FSharp.Compiler.Service.dll is loaded)
printfn "copy FSharp.Compiler.Interactive.Settings ..." 
let targetFCIS = __SOURCE_DIRECTORY__ + "/../../FAKE/tools/FSharp.Compiler.Interactive.Settings.dll"
if not (System.IO.File.Exists(targetFCIS)) then
  System.IO.File.Copy(__SOURCE_DIRECTORY__ + "/../lib/net40/FSharp.Compiler.Interactive.Settings.dll", targetFCIS) 

printfn "load FSharp.Formatting ..." 
printfn "load FSharp.Compiler.Service ..."
printfn "load FsReveal.dll"

FsReveal.FsRevealHelper.Folder <- __SOURCE_DIRECTORY__
printfn "Set FsReveal folder to %s" FsReveal.FsRevealHelper.Folder