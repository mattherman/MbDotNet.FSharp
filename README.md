# MbDotNet.FSharp

An F# wrapper around [MbDotNet](https://github.com/mattherman/MbDotNet), which is a .NET client library
for interacting with [mountebank](https://www.mbtest.org). This project aims to allow use of MbDotNet in
F# with more idiomatic syntax.

A simple example:
```
let imposter = httpImposter 4545 "My Imposter"
imposter
    |> should
    |> returnStatus HttpStatusCode.OK
    |> onPathAndMethodEqual "/test" Method.Get
```

## NuGet Package

The library will be available as a NuGet package once enough of the API has been wrapped.

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