$pattern = '\([a-zA-Z]+\.[a-zA-Z]+,\s+"[a-zA-Z]+",\s+\d+,\s+\(\d+,\s+\d+\)\),'

$file = "$PSScriptRoot/../src/Arborist.Linq.Tests/WhereTests.cs"

$lines = Get-Content $file

$lines.Count

$lines `
| ForEach-Object {
  if ($_ -match $pattern) {
    $originalPosition = $_.Substring($_.Length - 8, 6)

    $result = $_.Substring(0, $_.Length - 2)
    $result += ', '
    $result += $originalPosition
    $result += '),'
    $result
  }
  else {
    $_
  }
} `
| Out-File "$PSScriptRoot/../temp/Test.cs"
