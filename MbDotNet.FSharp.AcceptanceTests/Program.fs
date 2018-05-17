// Learn more about F# at http://fsharp.org

open System.Net
open MbDotNet.FSharp.Imposters

[<EntryPoint>]
let main argv =
    let imposter = httpImposter 4545

    imposter |> should
        |> returnStatus HttpStatusCode.OK
        |> onPathAndMethodEqual "/test" Get
        |> ignore

    imposter |> should
        |> returnStatus HttpStatusCode.InternalServerError
        |> onPathAndMethodEqual "/error" Get
        |> ignore
    
    create imposter

    0 // return an integer exit code
