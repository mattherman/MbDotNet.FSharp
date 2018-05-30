open System.Net
open MbDotNet.FSharp.Imposter
open MbDotNet.FSharp.Imposter.Http
open MbDotNet.FSharp.Imposter.Tcp

let canCreateAndGetHttpImposter =
    let port = 4500
    let imposter = httpImposter port

    imposter |> Http.should
        |> returnStatus HttpStatusCode.OK
        |> onPathAndMethodEqual "/test" Get
        |> ignore

    imposter |> Http.should
        |> returnStatus HttpStatusCode.InternalServerError
        |> onPathAndMethodEqual "/error" Get
        |> ignore
    
    create imposter

    match getHttpImposter port with
        | Some(_) -> true
        | None -> false

let canCreateAndGetHttpsImposter =
    let port = 4501
    let imposter = httpsImposter port

    imposter |> Https.should
        |> returnStatus HttpStatusCode.OK
        |> onPathAndMethodEqual "/test" Get
        |> ignore

    imposter |> Https.should
        |> returnStatus HttpStatusCode.InternalServerError
        |> onPathAndMethodEqual "/error" Get
        |> ignore
    
    create imposter

    match getHttpsImposter port with
        | Some(_) -> true
        | None -> false

let canCreateAndGetTcpImposter =
    let port = 4502
    let imposter = tcpImposter port

    imposter |> Tcp.should
        |> returnData "responsedata"
        |> onDataEquals "requestdata"
        |> ignore

    create imposter

    match getTcpImposter port with
        | Some(_) -> true
        | None -> false

let runTest (testFunc, testFuncName) =
    deleteAll

    let result = testFunc
    match result with
        | true -> printfn "PASSED %s" testFuncName
        | false -> printfn "FAILED %s" testFuncName

    result

let tests = [
    (canCreateAndGetHttpImposter, "canCreateAndGetHttpImposter");
    (canCreateAndGetHttpsImposter, "canCreateAndGetHttpsImposter");
    (canCreateAndGetTcpImposter, "canCreateAndGetTcpImposter");
]

[<EntryPoint>]
let main argv =
    List.map runTest tests 
        |> List.countBy (fun result -> if result then "PASS" else "FAIL")
        |> printfn "%A"

    0 // return an integer exit code