open System.IO

let readFile (path: string) =
    let basePath = Directory.GetCurrentDirectory()
    let filePath = Path.Combine(basePath, path)
    File.ReadAllLines(filePath)


let twoDArray (line: string array)=
    line
    |> Array.map (_.ToCharArray())
    |> array2D


let data = readFile "example.txt" |> twoDArray

let indices =
    data
    |> Array2D.mapi (fun x y _ -> (x, y))
    |> Seq.cast<(int * int)>
    |> Seq.toList

let ex = 1
let part1 = 1
let part2 = 1

printfn $"Example: %i{ex}"
printfn $"Part 1: %i{part1}"
printfn $"Part 2: %i{part2}"
