let FS_FORMATTING_VER = "2.4.20"
let FS_COMPILER_SERVICE_VER = "0.0.57" 

#I @"..\..\FSharp.Formatting.2.4.20\lib\net40\"
#I @"..\..\FSharp.Compiler.Service.0.0.57\lib\net40\"
#r "FSharp.Compiler.Service.dll"
#r "FSharp.Literate.dll"
#r @"..\lib\net40\FsReveal.dll"

printfn "load FSharp.Formatting %s.." FS_FORMATTING_VER
printfn "load FSharp.Compiler.Service %s.." FS_COMPILER_SERVICE_VER
printfn "load FsReveal.dll"
 
FsReveal.FsRevealHelper.Folder <- __SOURCE_DIRECTORY__

printfn "Set FsReveal folder to %s" FsReveal.FsRevealHelper.Folder