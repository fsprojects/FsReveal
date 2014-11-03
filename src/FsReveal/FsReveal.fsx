#I @"../../FSharp.Formatting/lib/net40/"
#I @"../../FSharp.Compiler.Service/lib/net40/"
#I @"../lib/net40/"
#r "FSharp.Compiler.Service.dll"
#r "FSharp.Literate.dll"
#r "FsReveal.dll"

printfn "load FSharp.Formatting ..." 
printfn "load FSharp.Compiler.Service ..."
printfn "load FsReveal.dll"
 
FsReveal.FsRevealHelper.Folder <- __SOURCE_DIRECTORY__

printfn "Set FsReveal folder to %s" FsReveal.FsRevealHelper.Folder
