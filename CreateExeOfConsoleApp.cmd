
SET outputDir=.\ExecutableFiles
SET exeFileName=Squirrel.ConsoleApp
SET exeCustomFileNamePrefix=Squirrel
SET pathToSln=.\backend\Squirrel.ConsoleApp.sln

del /s /q "%outputDir%"

dotnet publish "%pathToSln%" -r win-x64 -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:PublishTrimmed=true -p:EnableCompressionInSingleFile=true --output "%outputDir%"
ren "%outputDir%\%exeFileName%.exe" "%exeCustomFileNamePrefix%-win-x64.exe"

dotnet publish "%pathToSln%" -r win-x86 -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:PublishTrimmed=true -p:EnableCompressionInSingleFile=true --output "%outputDir%"
ren "%outputDir%\%exeFileName%.exe" "%exeCustomFileNamePrefix%-win-x86.exe"
