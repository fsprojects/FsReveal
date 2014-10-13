#I @"..\..\packages\FSharp.Formatting\lib\net40\"
#I @"..\..\packages\FSharp.Compiler.Service\lib\net40\"
#r "FSharp.Compiler.Service.dll"
#r "FSharp.Literate.dll"
#r @"..\lib\net40\FsReveal.dll"

printfn "load FSharp.Formatting ..." 
printfn "load FSharp.Compiler.Service ..."
printfn "load FsReveal.dll"
 
FsReveal.FsRevealHelper.Folder <- __SOURCE_DIRECTORY__

printfn "Set FsReveal folder to %s" FsReveal.FsRevealHelper.Folder