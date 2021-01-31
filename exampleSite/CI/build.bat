@echo off
set scriptdir=%~dp0
set hugodir=%~dp0..
pushd %hugodir%

if "%1" == "" (
  echo "arg1:environment was not specified. Build with default environment (chm) ..."
  set hugo_env=chm
) else (
  set hugo_env=%1
)

:: environment exist check
if not exist .\config\%hugo_env% (
  echo "%hugo_env% does not exist in config directory."
  exit /B 1
)

:: -----------------------
:: HUGO Build
:: -----------------------
echo Build %hugo_env% environment...
hugo --environment %hugo_env%
if not %errorlevel% == 0 exit /B 1

:: ----------
:: JP Help
:: ----------
:: convert to sjis
pushd %scriptdir%
powershell -NoProfile -ExecutionPolicy RemoteSigned ^
  ".\UTF8toLocalCode.ps1 -dir \"%hugodir%\public_%hugo_env%\jp\" -codename shift_jis -custom_table utf8_to_shift_jis.csv -logname unknown_%hugo_env%.log;exit $LASTEXITCODE"
if not %errorlevel% == 0 exit /B 1
popd

hhc.exe public_%hugo_env%\jp\project.hhp
:: hhc.exe returns 1 on success  https://ja.coder.work/so/cmd/1267049
if not %errorlevel% == 1 exit /B 1

:: ----------
:: EN Help
:: ----------
hhc.exe public_%hugo_env%\en\project.hhp
if not %errorlevel% == 1 exit /B 1

popd

echo build was completed.
exit /B 0