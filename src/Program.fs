open System
open System.IO
open System.Text.RegularExpressions

let digitValues =
    [| ("one", 1)
       ("two", 2)
       ("three", 3)
       ("four", 4)
       ("five", 5)
       ("six", 6)
       ("seven", 7)
       ("eight", 8)
       ("nine", 9) |]

let rec extractFirstNumber (text: string) : int =
    let index = 0

    if Char.IsDigit text[index] then
        Int32.Parse(text[index].ToString())
    else
        let hasMatch = digitValues |> Array.filter (fun d -> text.StartsWith(fst d))

        if hasMatch.Length > 0 then
            snd hasMatch[0]
        else
            let t = text.Substring(index + 1, text.Length - 1)
            extractFirstNumber (t)

let rec extractLastNumber (text: string) : int =
    let index = text.Length - 1

    if Char.IsDigit text[index] then
        Int32.Parse(text[index].ToString())
    else
        let hasMatch = digitValues |> Array.filter (fun d -> text.EndsWith(fst d))

        if hasMatch.Length > 0 then
            snd hasMatch[0]
        else
            let t = text.Substring(0, text.Length - 1)
            extractLastNumber (t)

let extractDigits (text: string) : int =
    let firstDigit = extractFirstNumber text
    let lastDigit = extractLastNumber text
    let combinedString = firstDigit.ToString() + lastDigit.ToString()
    Int32.Parse(combinedString)

let sum =
    File.ReadAllLines("day-1.txt")
    |> Array.map (fun elem -> extractDigits elem)
    // |> Array.iter (fun elem -> printfn $"{elem}")
    |> Array.reduce (fun acc elem -> acc + elem)

let result = sum
printfn $"{result}"
