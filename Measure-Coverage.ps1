#!/usr/bin/env pwsh
$currentDir = Get-Location
try {
    Set-Location $PSScriptRoot

    # install reportgenerator if not installed
    if(dotnet tool list --tool-path .dotnet/ `
        | Select-String dotnet-reportgenerator-globaltool) {
    }
    else {
        dotnet tool install dotnet-reportgenerator-globaltool --tool-path .dotnet/
    }

    # remove old test report
    Get-ChildItem -Directory src/*.Test*/TestResults/* | Remove-Item -Recurse

    # test and measure coverage
    dotnet test src/SmartVectorDotNet.slnx --collect:"XPlat Code Coverage"

    # export HTML coverage report
    Get-ChildItem src/*.Test*/TestResults/*/coverage.cobertura.xml `
        | ForEach-Object {
            $name = $_.FullName -replace '^.+[/\\](.+?)[/\\]TestResults[/\\].+[/\\]coverage.cobertura.xml$', '$1'
            &./.dotnet/reportgenerator -reports:"$_" -targetdir:".CodeCoverage/$name" -reporttypes:Html
        }
} finally {
    Set-Location $currentDir
}