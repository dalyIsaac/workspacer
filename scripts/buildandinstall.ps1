# This script builds and installs workspacer on the local computer.

param (
    [Parameter(Mandatory)]
    [string]$version
)

# Version format: <workspacer-version>-unstable-dalyisaac

$env:Path += ";C:\Program Files (x86)\WiX Toolset v3.11\bin"

Remove-Item out -Recurse

dotnet restore
dotnet publish /p:DefineConstants=BRANCH_unstable /p:Version=$version --configuration Release --no-restore
.\scripts\buildinstaller.ps1 -buildDir .\src\workspacer\bin\Release\net5.0-windows\win10-x64\publish\ -version $version

& ".\out\workspacer-${version}.msi"