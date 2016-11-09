﻿namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("FsReveal")>]
[<assembly: AssemblyProductAttribute("FsReveal")>]
[<assembly: AssemblyDescriptionAttribute("FsReveal parses markdown or F# script files and generates reveal.js slides.")>]
[<assembly: AssemblyVersionAttribute("1.4.0")>]
[<assembly: AssemblyFileVersionAttribute("1.4.0")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "1.4.0"
    let [<Literal>] InformationalVersion = "1.4.0"
