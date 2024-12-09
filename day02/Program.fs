open System
open System.IO

let readFile (path: string) =
    let basePath = Directory.GetCurrentDirectory()
    let filePath = Path.Combine(basePath, path)
    File.ReadAllLines(filePath)

let toIntList (line: String) = line.Split(" ") |> Array.toList |> List.map int

let bounds (x, y) = List.contains (abs (x - y)) [1..3]
let increasing (x,y) = (x < y)
let decreasing (x,y) = (x > y)

let isSafe parts =
    let pairs = List.pairwise parts
    let bounds = List.forall bounds pairs
    let inc = List.forall increasing pairs
    let dec = List.forall decreasing pairs
    bounds && (inc || dec)

let isSafeDamp parts =
    List.indexed parts
    |> List.map (fun (idx, _) -> List.removeAt idx parts)
    |> List.exists isSafe

let parse filename = readFile filename |> Array.toList |> List.map toIntList

let example = parse "example.txt"
let input = parse "input.txt"

let ex = example |> List.filter isSafe |> List.length
let part1 = input |> List.filter isSafe |> List.length
let part2 = input |> List.filter isSafeDamp |> List.length

printfn $"Example: %i{ex}"
printfn $"Part 1: %i{part1}"
printfn $"Part 2: %i{part2}"
