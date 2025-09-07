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
    dotnet test src/SmartVectorDotNet.slnx --collect:"XPlat Code Coverage" --settings src/etc/coverlet.runsettings

    # export HTML coverage report
    &./.dotnet/reportgenerator -reports:"src/*.Test*/TestResults/*/coverage.cobertura.xml" -targetdir:".CodeCoverage" -reporttypes:Html
} finally {
    Set-Location $currentDir
}
