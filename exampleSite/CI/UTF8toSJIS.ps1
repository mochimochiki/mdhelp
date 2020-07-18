using namespace UTF8toSJISConverter;

param(
  [String]$dir = "public"
)

$scriptPath = $MyInvocation.MyCommand.Path
$scriptDir = Split-Path -Parent $scriptPath
Add-Type -Path "$($scriptDir)\UTF8toSJISConverter.cs"
$logDir = $scriptDir
$logPath = Join-Path $logDir "UTF8toSJIS_UnknonwChars.log"
if (Test-Path $logPath) {
  Remove-Item $logPath
}

Push-Location $scriptDir\..

Write-Output @"
-----------------------
UTF8 to SJIS Converter
target dir: $($dir)
unknown chars log: $($logpath)
-----------------------
"@

$fileNum = 0
$unknonwFileNum = 0
Get-ChildItem $dir -recurse -include *.hhp, *.html, *.hhc | ForEach-Object {
  if ($_.GetType().Name -eq "FileInfo") {
    $inputPath = $_.FullName
    try {
      Write-Output "convert: $($inputPath)"
      $notExistUnknonw = [UTF8toSJISConverter]::Convert($inputPath, $inputPath, $logPath)
      if (!$notExistUnknonw) {
        $unknonwFileNum++
      }
      $fileNum++
    }
    catch {
      Write-Output $_.Exception.Message
      exit 1
    }
  }
}

Write-Output @"
Convertion completed !
Number of processed files: $($fileNum)
Number of unknwown chars files: $($unknonwFileNum)
"@

Pop-Location