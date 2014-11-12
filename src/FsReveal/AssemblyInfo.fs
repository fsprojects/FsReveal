namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("FsReveal")>]
[<assembly: AssemblyProductAttribute("FsReveal")>]
[<assembly: AssemblyDescriptionAttribute("FsReveal parses markdown or F# script files and generates reveal.js slides.")>]
[<assembly: AssemblyVersionAttribute("0.1.2")>]
[<assembly: AssemblyFileVersionAttribute("0.1.2")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.1.2"
