
:: Setting common parameters
SET outputDir=.\ExecutableFiles\SquirrelSetup-osx-x64
SET fileName=Squirrel.ConsoleApp
SET customFileNamePrefix=Squirrel
SET pathToSln=.\backend\Squirrel.ConsoleApp.sln

:: Script body
del /s /q "%outputDir%"

dotnet publish "%pathToSln%" -r osx-x64 -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:PublishTrimmed=true -p:EnableCompressionInSingleFile=true --output "%outputDir%"
ren "%outputDir%\%fileName%" "%customFileNamePrefix%-osx-x64"
