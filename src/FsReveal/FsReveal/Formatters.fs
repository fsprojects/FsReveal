module Formatters

open System.IO
open Deedle
open Deedle.Internal
open FSharp.Literate
open FSharp.Markdown
open FSharp.Charting


let startColumnCount = 3
let endColumnCount = 3

let startRowCount = 8
let endRowCount = 4

// How many items from a series should be rendered
let startItemCount = 5
let endItemCount = 3

// How many columns and rows from a matrix should be rendered
let matrixStartColumnCount = 5
let matrixEndColumnCount = 2
let matrixStartRowCount = 10
let matrixEndRowCount = 4

// How many items from a vector should be rendered
let vectorStartItemCount = 5
let vectorEndItemCount = 2

// --------------------------------------------------------------------------------------
// Helper functions etc.
// --------------------------------------------------------------------------------------

open System.Windows.Forms
open FSharp.Charting.ChartTypes

/// Extract values from any series using reflection
let (|SeriesValues|_|) (value:obj) = 
  let iser = value.GetType().GetInterface("ISeries`1")
  if iser <> null then
    let keys = value.GetType().GetProperty("Keys").GetValue(value) :?> System.Collections.IEnumerable
    let vector = value.GetType().GetProperty("Vector").GetValue(value) :?> IVector
    Some(Seq.zip (Seq.cast<obj> keys) vector.ObjectSequence)
  else None

/// Format value as a single-literal paragraph
let formatValue def = function
  | Some v -> [ Paragraph [Literal (v.ToString()) ]] 
  | _ -> [ Paragraph [Literal def] ]

/// Format body of a single table cell
let td v = [ Paragraph [Literal v] ]

/// Use 'f' to transform all values, then call 'g' with Some for 
/// values to show and None for "..." in the middle
let mapSteps (startCount, endCount) f g input = 
  input 
  |> Seq.map f |> Seq.startAndEnd startCount endCount
  |> Seq.map (function Choice1Of3 v | Choice3Of3 v -> g (Some v) | _ -> g None)
  |> List.ofSeq

// Tuples with the counts, for easy use later on
let fcols = startColumnCount, endColumnCount
let frows = startRowCount, endRowCount
let sitms = startItemCount, endItemCount
let mcols = matrixStartColumnCount, matrixEndColumnCount
let mrows = matrixStartRowCount, matrixEndRowCount
let vitms = vectorStartItemCount, vectorEndItemCount

/// Reasonably nice default style for charts
let chartStyle ch =
  let grid = ChartTypes.Grid(LineColor=System.Drawing.Color.LightGray)
  ch 
  |> Chart.WithYAxis(MajorGrid=grid)
  |> Chart.WithXAxis(MajorGrid=grid)

/// Checks if the given directory exists. If not then this functions creates the directory.
let ensureDirectory dir =
  let di = new DirectoryInfo(dir)
  if not di.Exists then di.Create()

/// Combine two paths
let (@@) a b = Path.Combine(a, b)

// --------------------------------------------------------------------------------------
// Handling of R
// --------------------------------------------------------------------------------------

open RDotNet
open RProvider
open RProvider.graphics
open RProvider.grDevices
open System.Drawing
open System

type ExtraEvaluationResult = 
  { Results : IFsiEvaluationResult
    CapturedImage : Bitmap option }
  interface IFsiEvaluationResult

let isEmptyBitmap (img:Bitmap) =
  seq { 
    let bits = img.LockBits(Rectangle(0,0,img.Width, img.Height), Imaging.ImageLockMode.ReadOnly, Imaging.PixelFormat.Format32bppArgb)
    let ptr0 = bits.Scan0 : IntPtr
    let stride = bits.Stride
    for i in 0 .. img.Width - 1 do
      for j in 0 .. img.Height - 1 do
        let offset = i*4 + stride*j
        if System.Runtime.InteropServices.Marshal.ReadInt32(ptr0,offset) <> -1 then
          yield false }
  |> Seq.isEmpty            

let captureDevice f = 
  let file = Path.GetTempFileName() + ".png"   
  let isRavailable =
    try R.png(file) |> ignore; true 
    with _ -> false

  let res = f()
  let img = 
    if isRavailable then
      R.dev_off() |> ignore
      let bmp = Image.FromStream(new MemoryStream(File.ReadAllBytes file)) :?> Bitmap
      File.Delete(file)
      if isEmptyBitmap bmp then None else Some bmp 
    else None

  { Results = res; CapturedImage = img } :> IFsiEvaluationResult

// --------------------------------------------------------------------------------------
// Handling of Math.NET Numerics Matrices
// --------------------------------------------------------------------------------------

open MathNet.Numerics
open MathNet.Numerics.LinearAlgebra

let formatMatrix (formatValue: 'T -> string) (matrix: Matrix<'T>) =
  let mappedColumnCount = min (matrixStartColumnCount + matrixEndColumnCount + 1) matrix.ColumnCount
  String.concat Environment.NewLine
    [ "\\begin{bmatrix}"
      matrix.EnumerateRows()
        |> mapSteps mrows id (function
          | Some row -> row |> mapSteps mcols id (function Some v -> formatValue v | _ -> "\\cdots") |> String.concat " & "
          | None -> Array.zeroCreate matrix.ColumnCount |> mapSteps mcols id (function Some v -> "\\vdots" | _ -> "\\ddots") |> String.concat " & ")
        |> String.concat ("\\\\ " + Environment.NewLine)
      "\\end{bmatrix}" ]

let formatVector (formatValue: 'T -> string) (vector: Vector<'T>) =
  String.concat Environment.NewLine
    [ "\\begin{bmatrix}"
      vector.Enumerate()
        |> mapSteps vitms id (function | Some v -> formatValue v | _ -> "\\cdots")
        |> String.concat " & "
      "\\end{bmatrix}" ]

// --------------------------------------------------------------------------------------
// Build FSI evaluator
// --------------------------------------------------------------------------------------

let mutable currentOutputKind = OutputKind.Html
let InlineMultiformatBlock(html, latex) =
  let block =
    { new MarkdownEmbedParagraphs with
        member x.Render() =
          if currentOutputKind = OutputKind.Html then [ InlineBlock html ] else [ InlineBlock latex ] }
  EmbedParagraphs(block)

let MathDisplay(latex) = Span [ LatexDisplayMath latex ]

/// Builds FSI evaluator that can render System.Image, F# Charts, series & frames
let createFsiEvaluator root output =

  /// Counter for saving files
  let imageCounter = 
    let count = ref 0
    (fun () -> incr count; !count)

  let transformation (value:obj, typ:System.Type) =
    match value with 
    | :? System.Drawing.Image as img ->
        // Pretty print image - save the image to the "images" directory 
        // and return a DirectImage reference to the appropriate location
        let id = imageCounter().ToString()
        let file = "chart" + id + ".png"        
        ensureDirectory (output @@ "images")
        img.Save(output @@ "images" @@ file, System.Drawing.Imaging.ImageFormat.Png) 
        Some [ Paragraph [DirectImage ("", (root + "/images/" + file, None))]  ]

    | :? ChartTypes.GenericChart as ch ->
        // Pretty print F# Chart - save the chart to the "images" directory 
        // and return a DirectImage reference to the appropriate location
        let id = imageCounter().ToString()
        let file = "chart" + id + ".png"
        ensureDirectory (output @@ "images")
      
        // We need to reate host control, but it does not have to be visible
        ( use ctl = new ChartControl(chartStyle ch, Dock = DockStyle.Fill, Width=500, Height=300)
          ch.CopyAsBitmap().Save(output @@ "images" @@ file, System.Drawing.Imaging.ImageFormat.Png) )
        Some [ Paragraph [DirectImage ("", (root + "/images/" + file, None))]  ]

    | SeriesValues s ->
        // Pretty print series!
        let heads  = s |> mapSteps sitms fst (function Some k -> td (k.ToString()) | _ -> td " ... ")
        let row    = s |> mapSteps sitms snd (function Some v -> formatValue "N/A" (OptionalValue.asOption v) | _ -> td " ... ")
        let aligns = s |> mapSteps sitms id (fun _ -> AlignDefault)
        [ InlineMultiformatBlock("<div class=\"deedleseries\">", "\\vspace{1em}")
          TableBlock(Some ((td "Keys")::heads), AlignDefault::aligns, [ (td "Values")::row ]) 
          InlineMultiformatBlock("</div>","\\vspace{1em}") ] |> Some

    | :? IFrame as f ->
      // Pretty print frame!
      {new IFrameOperation<_> with
        member x.Invoke(f) = 
          let heads  = f.ColumnKeys |> mapSteps fcols id (function Some k -> td (k.ToString()) | _ -> td " ... ")
          let aligns = f.ColumnKeys |> mapSteps fcols id (fun _ -> AlignDefault)
          let rows = 
            f.Rows |> Series.observationsAll |> mapSteps frows id (fun item ->
              let def, k, data = 
                match item with 
                | Some(k, Some d) -> "N/A", k.ToString(), Series.observationsAll d |> Seq.map snd 
                | Some(k, _) -> "N/A", k.ToString(), f.ColumnKeys |> Seq.map (fun _ -> None)
                | None -> " ... ", " ... ", f.ColumnKeys |> Seq.map (fun _ -> None)
              let row = data |> mapSteps fcols id (function Some v -> formatValue def v | _ -> td " ... ")
              (td k)::row )
          Some [ 
            InlineMultiformatBlock("<div class=\"deedleframe\">","\\vspace{1em}")
            TableBlock(Some ([]::heads), AlignDefault::aligns, rows) 
            InlineMultiformatBlock("</div>","\\vspace{1em}")
          ] }
      |> f.Apply

    | :? Matrix<double> as m -> Some [ MathDisplay (m |> formatMatrix (fun x -> x.ToString("G6"))) ]
    | :? Matrix<float> as m -> Some [ MathDisplay (m |> formatMatrix (fun x -> x.ToString("G3"))) ]
    | :? Vector<double> as v -> Some [ MathDisplay (v |> formatVector (fun x -> x.ToString("G6"))) ]
    | :? Vector<float> as v -> Some [ MathDisplay (v |> formatVector (fun x -> x.ToString("G3"))) ]

    | _ -> None 
    
  // Create FSI evaluator, register transformations & return
  let fsiEvaluator = FsiEvaluator() 
  fsiEvaluator.RegisterTransformation(transformation)
  let fsiEvaluator = fsiEvaluator :> IFsiEvaluator
  { new IFsiEvaluator with
      member x.Evaluate(text, asExpr, file) = 
        captureDevice (fun () -> 
          fsiEvaluator.Evaluate(text, asExpr, file))

      member x.Format(res, kind) = 
        let res = res :?> ExtraEvaluationResult
        match kind, res.CapturedImage with
        | FsiEmbedKind.Output, Some img -> 
            [ match (res.Results :?> FsiEvaluationResult).Output with
              | Some s  when not (String.IsNullOrWhiteSpace(s)) ->
                  yield! fsiEvaluator.Format(res.Results, kind)
              | _ -> ()
              yield! transformation(img, typeof<Image>).Value ]
        | _ -> fsiEvaluator.Format(res.Results, kind) }

