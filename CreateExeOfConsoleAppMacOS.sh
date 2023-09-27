#!/bin/bash

# Setting common parameters
outputDir="./ExecutableFiles/SquirrelSetup-osx-x64"
fileName="Squirrel.ConsoleApp"
customFileNamePrefix="Squirrel"
pathToSln="./backend/Squirrel.ConsoleApp.sln"

# Script body
rm -rf "$outputDir"

dotnet publish "$pathToSln" -r osx-x64 -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:PublishTrimmed=true -p:EnableCompressionInSingleFile=true --output "$outputDir"

# Rename the output file
mv "$outputDir/$fileName" "$outputDir/$customFileNamePrefix-osx-x64"
