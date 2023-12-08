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

let removeDigits (input: string) =
    let pattern = "\\d" // Regular expression to match digits
    let regex = new Regex(pattern)
    regex.Replace(input, "")

let replaceAtIndex (index: int, text: string, digitAsText: string, digitAsNumber: int) =
    if (index + digitAsText.Length <= text.Length) then
        let potentialMatch = text.Substring(index, digitAsText.Length)

        if digitAsText = potentialMatch then
            text.Substring(0, index)
            + digitAsNumber.ToString()
            + text.Substring(index + digitAsText.Length, text.Length - index - digitAsText.Length)
        else
            text
    else
        text

let isMatchAtIndex (index: int, text: string, digitAsText: string, digitAsNumber: int) : bool =
    (index + digitAsText.Length <= text.Length
     && text.Substring(index, digitAsText.Length) = digitAsText)

let rec replaceNumberToDigit (index: int, text: string) : string =
    if (index < text.Length - 1) then
        let hasMatch =
            digitValues
            |> Array.filter (fun d -> isMatchAtIndex (index, text, fst d, snd d))

        if hasMatch.Length > 0 then
            let newText = replaceAtIndex (index, text, fst hasMatch[0], snd hasMatch[0])
            replaceNumberToDigit (0, newText)
        else
            replaceNumberToDigit (index + 1, text)
    else
        text

let extractDigits (text: string) : int =
    let firstDigit = text.ToCharArray() |> Array.tryFind (fun c -> Char.IsDigit c)
    let lastDigit = text.ToCharArray() |> Array.tryFindBack (fun c -> Char.IsDigit c)

    if (firstDigit.IsSome && lastDigit.IsSome) then
        let newDigit = String [| firstDigit.Value; lastDigit.Value |]
        Int32.Parse(newDigit)
    else
        0

let sum =
    File.ReadAllLines("day-1.txt")
    |> Array.map (fun elem -> replaceNumberToDigit (0, elem))
    |> Array.map (fun elem -> extractDigits elem)
    // |> Array.iter (fun elem -> printfn $"{elem}")
    |> Array.reduce (fun acc elem -> acc + elem)

// let result = isMatchAtIndex (4, "eightwothree", "two", 2)
let result = sum
printfn $"{result}"
