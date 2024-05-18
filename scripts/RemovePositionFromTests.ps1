$pattern = '\(\d+,\s+\d+\),\s+\(\d+,\s+\d+\)'

$file = "$PSScriptRoot/../src/Arborist.Tests/TreeTraversalTestData.cs"
#$file = "$PSSCRiptRoot/../src/Arborist.Linq.Tests/PruneAfterTests.cs"
#$file = "$PSSCRiptRoot/../src/Arborist.Linq.Tests/PruneBeforeTests.cs"
#$file = "$PSSCRiptRoot/../src/Arborist.Linq.Tests/WhereTests.cs"

$lines = Get-Content $file

$lines.Count

$lines `
| ForEach-Object {
  if ($_ -match $pattern) {
    $originalPosition = $_.Substring($_.Length - 16, 6)

    $result = $_.Substring(0, $_.Length - 18)
    $result += ', '
    $result += $originalPosition
    $result += '),'
    $result
  }
  else {
    $_
  }
} `
| Out-File "$PSScriptRoot/../local-temp/Test.cs"
