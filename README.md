# MbDotNet.FSharp

An F# wrapper around [MbDotNet](https://github.com/mattherman/MbDotNet), which is a .NET client library
for interacting with [mountebank](https://www.mbtest.org). This project aims to allow use of MbDotNet in
F# with more idiomatic syntax.

A simple example:
```
open MbDotNet.FSharp.Imposters
open MbDotNet.FSharp.Imposters.Http

let imposter = httpImposter 4545

// Return JSON on a GET request to '/test'
imposter
    |> should
    |> returnJson HttpStatusCode.OK myResponseObject
    |> onPathAndMethodEqual "/test" Method.Get
    |> ignore

// Return a 404 on any other request
imposter
    |> should
    |> returnStatus HttpStatusCode.NotFound
    |> ignore

create imposter
```

## NuGet Package

The library is available for install as a NuGet package:

https://www.nuget.org/packages/MbDotNet.FSharp

For now, only prerelease versions are available as the API may change over time, however, it is definitely usable
in its current state and supports basic stubs for HTTP, HTTPS, and TCP imposters.

## Development

### Prerequisites

The following items are necessary in order to build/test the project:
* .NET Core SDK 2.0
* .NET Core Runtime 2.0
* Mountebank

### Building

To build the project, run the following from the root directory:
```
dotnet build
```

The solution includes a set of acceptance tests that run against an actual mountebank instance.

In order to run the acceptance tests, run the following command from the root directory:
```
dotnet run --project ./MbDotNet.Acceptance.Tests
```