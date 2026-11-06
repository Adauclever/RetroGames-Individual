# Execute com ponto e espaço: . ./scripts/usar_node22.ps1
$pastaProjeto = Split-Path $PSScriptRoot -Parent
$pastaNode = Get-ChildItem (Join-Path $pastaProjeto '.ferramentas') -Directory -Filter 'node-v22*-win-x64' -ErrorAction SilentlyContinue | Select-Object -First 1
if (-not $pastaNode) { throw 'Node 22 portátil não encontrado. Instale Node.js 22 globalmente ou adicione a distribuição oficial em .ferramentas.' }
$env:PATH = $pastaNode.FullName + [IO.Path]::PathSeparator + $env:PATH
node --version
npm --version
