namespace advent

module Day2 =
    open System.IO
    open System

    type ColourCubeInfo = { Colour: string; Count: int }
    type GameSet = { ColourCubes: ColourCubeInfo[] }

    type Game =
        { Name: string
          Number: int
          GameSets: GameSet[] }

    let parseColourCubeInfo (text: string) : ColourCubeInfo =
        let values = text.Trim().Split(' ') // Splits on space eg. 14 green

        { Colour = values[1]
          Count = Int32.Parse(values[0]) }

    let parseGameSet (text: string) : GameSet =
        let values = text.Split(',') // Splits eg. 7 blue, 1 red, 14 green

        let colourCubes =
            values |> Seq.map (fun item -> parseColourCubeInfo item) |> Seq.toArray

        { ColourCubes = colourCubes }

    let parse (text: string) =
        let init = text.Split(':') // Splits on : eg. Game 2: 6 blue, 3 green; 4 red, 1 green, 7 blue; 2 green
        let gameContent = init[1]
        let gameSetValues = gameContent.Split(';') // Splits on ; eg. 6 blue, 3 green; 4 red, 1 green, 7 blue; 2 green

        let gameSets =
            gameSetValues |> Seq.map (fun elem -> parseGameSet elem) |> Seq.toArray

        printfn $"{gameSetValues}"

        let gameName = init[0]
        let gameNameDetails = gameName.Split(' ')

        let game =
            { Name = gameNameDetails[0]
              Number = Int32.Parse(gameNameDetails[1])
              GameSets = gameSets }

        printfn $"{game}"
        text

    let run =
        printfn "day 2"
        // parseGameSet "7 blue, 1 red, 14 green"
        File.ReadAllLines("day-2.txt") |> Array.map (fun line -> parse line)
