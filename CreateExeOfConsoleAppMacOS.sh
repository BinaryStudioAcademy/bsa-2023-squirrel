#!/bin/bash

# Setting common parameters
outputDir="./ExecutableFiles/macOS"
exeFileName="Squirrel.ConsoleApp"
exeCustomFileNamePrefix="Squirrel"
pathToSln="./backend/Squirrel.ConsoleApp.sln"

# Script body
rm -rf "$outputDir"

dotnet publish "$pathToSln" -r osx-x64 -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:PublishTrimmed=true -p:EnableCompressionInSingleFile=true --output "$outputDir"
