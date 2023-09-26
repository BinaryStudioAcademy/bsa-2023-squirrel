
:: Setting common parameters
SET outputDir=.\ExecutableFiles\macOS
SET exeFileName=Squirrel.ConsoleApp
SET exeCustomFileNamePrefix=Squirrel
SET pathToSln=.\backend\Squirrel.ConsoleApp.sln

:: Script body
del /s /q "%outputDir%"

dotnet publish "%pathToSln%" -r osx-x64 -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:PublishTrimmed=true -p:EnableCompressionInSingleFile=true --output "%outputDir%"
