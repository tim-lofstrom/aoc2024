open System.Text.RegularExpressions
open System.IO

let readFile (path: string) =
    let basePath = Directory.GetCurrentDirectory()
    let filePath = Path.Combine(basePath, path)
    File.ReadAllLines(filePath)

let matches input =
    Regex.Matches(input, @"mul\(\d+,\d+\)")
    |> Seq.cast<Match>
    |> Seq.map _.Value
    |> Seq.toList

let matchesWithStatements input =
    Regex.Matches(input, @"mul\(\d+,\d+\)|don't\(\)|do\(\)")
    |> Seq.cast<Match>
    |> Seq.map _.Value
    |> Seq.toList

let extractNumbers instruction =
    let parts = Regex.Match(instruction, @"mul\((\d+),(\d+)\)")
    match parts with
    | part when part.Success ->
        let num1 = int part.Groups.[1].Value
        let num2 = int part.Groups.[2].Value
        (num1, num2)
    | _ -> failwith "Error"


let mul (x,y) = x*y

let calc1 file =
    let data = readFile file |> String.concat ""
    let numbers = matches data |> List.map extractNumbers
    numbers |> List.map mul |> List.reduce (+)


let folder (enabled: bool, acc: string list) (item: string) =
    match item with
    | "don't()" -> (false, acc)
    | "do()" -> (true, acc)
    | _ when enabled -> (enabled, item::acc)
    | _ ->  (enabled, acc)

let calc2 file =
    let data = readFile file |> String.concat ""
    let commands = matchesWithStatements data
    let (_enabled, result) = commands |> List.fold folder (true, [])
    let numbers = result |> List.map extractNumbers
    numbers |> List.map mul |> List.reduce (+)


let ex = calc1 "example.txt"
let part1 = calc1 "input.txt"
let part2 = calc2 "input.txt"

printfn $"Example: %i{ex}"
printfn $"Part 1: %i{part1}"
printfn $"Part 2: %i{part2}"
