open System
open System.IO

let readFile (path: string) =
    let basePath = Directory.GetCurrentDirectory()
    let filePath = Path.Combine(basePath, path)
    File.ReadAllLines(filePath)

let ex = 1
let part1 = 1
let part2 = 1

printfn $"Example: %i{ex}"
printfn $"Part 1: %i{part1}"
printfn $"Part 2: %i{part2}"
