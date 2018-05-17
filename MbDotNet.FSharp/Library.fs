namespace MbDotNet.FSharp

open MbDotNet
open MbDotNet.Models
open System

module Imposters =
    let client = new MountebankClient()

    let create (imposter: Imposters.Imposter) =
        client.Submit(imposter)

    let deleteAll =
        client.DeleteAllImposters()

    module Http =
        type Method = Get | Post | Patch | Delete | Put | Head | Trace | Options | Connect

        let parseMethodEnum input =
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

        let getHttpImposter port = 
            let imposter = client.GetHttpImposter(port)
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

    module Tcp =
        let tcpImposter port = client.CreateTcpImposter(Nullable<int>(port))
        
        let should (imposter: Imposters.TcpImposter) =
            imposter.AddStub()