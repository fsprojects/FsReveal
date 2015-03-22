namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("FsReveal")>]
[<assembly: AssemblyProductAttribute("FsReveal")>]
[<assembly: AssemblyDescriptionAttribute("FsReveal parses markdown or F# script files and generates reveal.js slides.")>]
[<assembly: AssemblyVersionAttribute("0.7.2")>]
[<assembly: AssemblyFileVersionAttribute("0.7.2")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.7.2"
