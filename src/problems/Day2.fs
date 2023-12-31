namespace advent

module Day2 =
    open System.IO
    open System

    type ColourCube = { Colour: string; Count: int }
    type GameSet = { ColourCubes: ColourCube[] }

    type Game =
        { Name: string
          Number: int
          GameSets: GameSet[] }

    let parseColourCubeInfo (text: string) : ColourCube =
        let values = text.Trim().Split(' ') // Splits on space eg. 14 green

        { Colour = values[1]
          Count = Int32.Parse(values[0]) }

    let parseGameSet (text: string) : GameSet =
        let values = text.Split(',') // Splits eg. 7 blue, 1 red, 14 green

        let colourCubes =
            values |> Seq.map (fun item -> parseColourCubeInfo item) |> Seq.toArray

        { ColourCubes = colourCubes }

    let parse (text: string) : Game =
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

        game

    let validateColourCube (colourCube: ColourCube) : bool =
        printfn $"{colourCube}"

        if colourCube.Colour = "red" && colourCube.Count > 12 then
            false
        elif colourCube.Colour = "green" && colourCube.Count > 13 then
            false
        elif colourCube.Colour = "blue" && colourCube.Count > 14 then
            false
        else
            true

    let validateGameSet (gameSet: GameSet) : bool =
        let validColourCubes =
            gameSet.ColourCubes
            |> Seq.filter (fun item -> validateColourCube item)
            |> Seq.toArray

        gameSet.ColourCubes.Length = validColourCubes.Length

    let validateGame (game: Game) : bool =
        let validGameSets =
            game.GameSets |> Seq.filter (fun item -> validateGameSet item) |> Seq.toArray

        game.GameSets.Length = validGameSets.Length

    let run =
        printfn "day 2"
        // parseGameSet "7 blue, 1 red, 14 green"
        let result =
            File.ReadAllLines("day-2.txt")
            |> Seq.map (fun line -> parse line)
            |> Seq.filter (fun item -> validateGame item)
            |> Seq.map (fun item -> item.Number)
            |> Seq.reduce (fun acc elem -> acc + elem)

        printfn $"{result}"
