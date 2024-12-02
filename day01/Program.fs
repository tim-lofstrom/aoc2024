open System
open System.IO

let readFileAsArray (path: string) =
    let basePath = Directory.GetCurrentDirectory()
    let filePath = Path.Combine(basePath, path)
    File.ReadAllLines(filePath)

let readFileAsString (path: string) =
    let basePath = Directory.GetCurrentDirectory()
    let filePath = Path.Combine(basePath, path)
    File.ReadAllText(filePath)

let input = readFileAsArray "input.txt"
let example = readFileAsArray "example.txt"

let transpose (array: int[][]): int[][] =
    if Array.isEmpty array then [||]
    else
        let numRows = array.Length
        let numCols = array.[0].Length
        Array.init numCols (fun colIndex ->
            Array.init numRows (fun rowIndex ->
                array.[rowIndex].[colIndex]))

let toIntArray = (fun (line: string) -> line.Split(' ', StringSplitOptions.RemoveEmptyEntries) |> Array.map int)

let sortParts (x: int[][]): int[][] =
    match x with
    | [| fst; snd |] -> [| Array.sort fst; Array.sort snd |]
    | _ -> failwith "Input must contain exactly two arrays"


let calculatePart1 input =
    let transposed = input |> Array.map toIntArray |> transpose
    let sortedTransposed = sortParts transposed
    transpose sortedTransposed

let distanceSum acc (curr: int[]) =
    match curr with
    | [| fst; snd |] -> Math.Abs(snd - fst) + acc
    | _ -> acc

let calc (acc, snd) (curr:int) =
    let occ = snd |> Array.filter (fun x -> x = curr) |> Array.length
    let newAcc = acc + (occ * curr);
    (newAcc, snd)

let similarityScore input =
    let transposed = input |> Array.map toIntArray |> transpose
    match transposed with
    | [| first; second |] -> first |> Array.fold calc (0, second) |> fst
    | _ -> failwith "Input must contain exactly two arrays"

let part1 = calculatePart1 input |> Array.fold distanceSum 0
let part2 = similarityScore input

printfn $"Part 1: %i{part1}"
printfn $"Part 2: %A{part2}"

printfn "Klar"


// Part1: 1882714