namespace advent

open System
open System.IO

module Day1 = 
    let extractDigits (text: string):int = 
        let firstDigit = 
            text.ToCharArray()
            |> Array.tryFind(fun c -> Char.IsDigit c)
        let lastDigit = 
            text.ToCharArray()
            |> Array.tryFindBack(fun c -> Char.IsDigit c)
        let newDigit = String [| firstDigit.Value;lastDigit.Value |]
        Int32.Parse(newDigit)

    let sum = 
        File.ReadAllLines("day-1.txt")
        |> Array.map (fun elem -> extractDigits elem)
        |> Array.reduce(fun acc elem -> acc + elem )