namespace MbDotNet.FSharp

open MbDotNet
open MbDotNet.Models
open System

module Imposter =
    let client = new MountebankClient()

    let create (imposter: Imposters.Imposter) =
        client.Submit(imposter)

    let delete port =
        client.DeleteImposter(port)

    let deleteAll =
        client.DeleteAllImposters()

    module Http =
        type Method = Get | Post | Patch | Delete | Put | Head | Trace | Options | Connect

        let private parseMethodEnum input =
            match input with
                | Get -> Enums.Method.Get
                | Post -> Enums.Method.Post
                | Patch -> Enums.Method.Patch
                | Delete -> Enums.Method.Delete
                | Put -> Enums.Method.Put
                | Head -> Enums.Method.Head
                | Trace -> Enums.Method.Trace
                | Options -> Enums.Method.Options
                | Connect -> Enums.Method.Connect

        type Header = string * string

        let httpImposter port = client.CreateHttpImposter(Nullable<int>(port))

        let httpsImposter port = client.CreateHttpsImposter(Nullable<int>(port))

        let getHttpImposter port = 
            let imposter = client.GetHttpImposter(port)
            match imposter with
                | null -> None
                | _ -> Some(imposter)

        let getHttpsImposter port = 
            let imposter = client.GetHttpsImposter(port)
            match imposter with
                | null -> None
                | _ -> Some(imposter)

        let should (imposter: Imposters.HttpImposter) =
            imposter.AddStub()

        let returnStatus status (stub: Stubs.HttpStub) =
            stub.ReturnsStatus(status)

        let returnBody status body (stub: Stubs.HttpStub) =
            stub.ReturnsBody(status, body)

        let returnJson status responseObject (stub: Stubs.HttpStub) =
            stub.ReturnsJson(status, responseObject)

        let returnXml status responseObject (stub: Stubs.HttpStub) =
            stub.ReturnsXml(status, responseObject)

        let returns status headers responseObject (stub: Stubs.HttpStub) =
            stub.Returns(status, dict headers, responseObject)

        let onPathEquals path (stub: Stubs.HttpStub) =
            stub.OnPathEquals(path)

        let onMethodEquals method (stub: Stubs.HttpStub) =
            stub.OnMethodEquals(parseMethodEnum method)

        let onPathAndMethodEqual path method (stub: Stubs.HttpStub) =
            stub.OnPathAndMethodEqual(path, parseMethodEnum method)

    module Https =
        let should (imposter: Imposters.HttpsImposter) =
            imposter.AddStub()

    module Tcp =
        let tcpImposter port = client.CreateTcpImposter(Nullable<int>(port))
        
        let should (imposter: Imposters.TcpImposter) =
            imposter.AddStub()

        let getTcpImposter port = 
            let imposter = client.GetTcpImposter(port)
            match imposter with
                | null -> None
                | _ -> Some(imposter)

        let returnData data (stub: Stubs.TcpStub) =
            stub.ReturnsData(data)

        let onDataEquals data (stub: Stubs.TcpStub) =
            stub.OnDataEquals(data)