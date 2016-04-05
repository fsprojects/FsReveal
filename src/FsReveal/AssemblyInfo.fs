namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("FsReveal")>]
[<assembly: AssemblyProductAttribute("FsReveal")>]
[<assembly: AssemblyDescriptionAttribute("FsReveal parses markdown or F# script files and generates reveal.js slides.")>]
[<assembly: AssemblyVersionAttribute("1.2.2")>]
[<assembly: AssemblyFileVersionAttribute("1.2.2")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "1.2.2"
    let [<Literal>] InformationalVersion = "1.2.2"
