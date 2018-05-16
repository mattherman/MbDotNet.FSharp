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