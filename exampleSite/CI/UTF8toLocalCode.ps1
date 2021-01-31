using namespace UTF8toLocalCodeConverter;

param(
  [String]$dir = "public",
  [String]$codename = "shift_jis",
  [String]$custom_table = "utf8_to_shift_jis.csv",
  [String]$logname = "UTF8toLocalCode_Unknown.log"
)

$scriptPath = $MyInvocation.MyCommand.Path
$scriptDir = Split-Path -Parent $scriptPath
Add-Type -Path "$($scriptDir)\UTF8toLocalCodeConverter.cs"
$logDir = $scriptDir
$logPath = Join-Path $logDir $logname
if (Test-Path $logPath) {
  Remove-Item $logPath
}
$custom_tablePath = Join-Path $scriptDir $custom_table
if (-Not (Test-Path $logPath)) {
  Write-Output "custom table is not exist."
}

Push-Location $scriptDir\..

Write-Output @"
-----------------------
UTF8 to $($codename) Converter
target dir: $($dir)
custom table: $($custom_table)
unknown chars log: $($logpath)
-----------------------
"@

$fileNum = 0
$unknonwFileNum = 0
Get-ChildItem $dir -recurse -include *.hhp, *.html, *.hhc | ForEach-Object {
  if ($_.GetType().Name -eq "FileInfo") {
    $inputPath = $_.FullName
    try {
      $notExistUnknonw = [UTF8toLocalCodeConverter]::Convert( $inputPath,
                                                              $inputPath,
                                                              $codename,
                                                              $custom_tablePath,
                                                              $logPath)
      if (!$notExistUnknonw) {
        $unknonwFileNum++
        Write-Output "convert: $($inputPath) --unknwon char is detected!!"
      }
      else {
        Write-Output "convert: $($inputPath)"
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
-------------------------------------------------
Convertion completed !
Number of processed files: $($fileNum)
Number of unknwown chars files: $($unknonwFileNum)
  * show $($logpath) for details.
-------------------------------------------------



"@

Pop-Location