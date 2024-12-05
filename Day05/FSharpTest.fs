open System
open System.IO
open System.Text.RegularExpressions

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

let lines = File.ReadAllLines("Day05TestInput.txt")

let pageOrderRegex = Regex(@"(\d+)\|(\d+)")
let pagesInUpdateRegex = Regex(@"(\d+)+")

let extractOrderedPages (line: string) =
    let numberMatch = pageOrderRegex.Match(line)
    if numberMatch.Success then
        let x = int numberMatch.Groups.[1].Value
        let y = int numberMatch.Groups.[2].Value
        Some (x, y)
    else
        None

let extractPagesInUpdate (line: string) =
    let numberMatches = pagesInUpdateRegex.Matches(line)
    numberMatches
    |> Seq.cast<Match>
    |> Seq.map (fun matchGroup -> int matchGroup.Value)
    |> Seq.toList

let blankLineIndex = lines |> Array.tryFindIndex (fun line -> line.Trim() = "")



let pageOrders, allPagesInUpdate =
    match blankLineIndex with
    | Some index ->
        let firstPart, secondPart = Array.splitAt index lines
        let pageOrders =
            firstPart 
            |> Array.choose extractOrderedPages
            |> Array.toList
        let allPagesInUpdate = 
            secondPart
            |> Array.skip 1
            |> Array.map extractPagesInUpdate
            |> Array.toList
        pageOrders, allPagesInUpdate
    | None ->
        [], []

printfn "%A" pageOrders
printfn "%A" allPagesInUpdate

