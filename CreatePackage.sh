#!/bin/sh

rm *.nupkg
dotnet pack ./MbDotNet.FSharp/MbDotNet.FSharp.fsproj -c Release -o ../