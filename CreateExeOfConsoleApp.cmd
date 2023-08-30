
:: Setting common parameters
SET outputDir=.\ExecutableFiles
SET exeFileName=Squirrel.ConsoleApp
SET exeCustomFileNamePrefix=Squirrel
SET pathToSln=.\backend\Squirrel.ConsoleApp.sln


:: Script body
del /s /q "%outputDir%"

call :PublishApp win-x64
call :PublishApp win-x86

exit /b


:: Subroutine description
:PublishApp
set architecture=%1

dotnet publish "%pathToSln%" -r %architecture% -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:PublishTrimmed=true -p:EnableCompressionInSingleFile=true --output "%outputDir%"
ren "%outputDir%\%exeFileName%.exe" "%exeCustomFileNamePrefix%-%architecture%.exe"
