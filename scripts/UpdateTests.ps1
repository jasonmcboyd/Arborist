$pattern = '\([a-zA-Z]+\.[a-zA-Z]+,\s+"[a-zA-Z]+",\s+\d+,\s+\(\d+,\s+\d+\),\s+\(\d+,\s+\d+\)\),'

$file = "$PSScriptRoot/../src/Arborist.Tests/TreeTraversalTestData.cs"

$lines = Get-Content $file

$lines.Count

$lines `
| ForEach-Object {
  if ($_ -match $pattern) {

    $result = $_.Substring(0, $_.Length - 10)
    $result += '),'
    $result
  }
  else {
    $_
  }
} `
| Out-File "$PSScriptRoot/../working/Test.cs"
