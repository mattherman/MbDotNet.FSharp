namespace MbDotNet.FSharp

open MbDotNet
open MbDotNet.Models
open MbDotNet.Enums
open System
open System.Net

module Imposters =
    let client = new MountebankClient()

    let httpImposter port =
        client.CreateHttpImposter(Nullable<int>(port))

    let should (imposter: Imposters.HttpImposter) =
        imposter.AddStub()

    let returnStatus (status: HttpStatusCode) (stub: Stubs.HttpStub) =
        stub.ReturnsStatus(status)

    let onPathAndMethodEqual path (method: Method) (stub: Stubs.HttpStub) =
        stub.OnPathAndMethodEqual(path, method)

    let create (imposter: Imposters.Imposter) =
        client.Submit(imposter)
