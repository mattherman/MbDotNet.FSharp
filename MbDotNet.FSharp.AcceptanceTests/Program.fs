// Learn more about F# at http://fsharp.org

open System.Net
open MbDotNet.FSharp.Imposters
open MbDotNet.FSharp.Imposters.Http

let canCreateAndGetHttpImposter =
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

let runTest (testFunc, testFuncName) =
    deleteAll
    printfn "RUNNING %s" testFuncName
    testFunc

let tests = [
    (canCreateAndGetHttpImposter, "canCreateAndGetHttpImposter")
]

[<EntryPoint>]
let main argv =
    List.map runTest tests |> ignore

    0 // return an integer exit code