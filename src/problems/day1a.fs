namespace advent

open System
open System.IO

module Day1a =

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

    let rec extractFirstNumber (text: string) : string =
        let index = 0

        if Char.IsDigit text[index] then
            text[index].ToString()
        else
            let hasMatch = digitValues |> Array.filter (fun d -> text.StartsWith(fst d))

            if hasMatch.Length > 0 then
                fst hasMatch[0]
            else
                let t = text.Substring(index + 1, text.Length - 1)
                extractFirstNumber (t)
