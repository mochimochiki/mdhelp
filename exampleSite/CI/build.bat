@echo off
pushd %~dp0..

:: -----------------------
:: Normal Edition
:: -----------------------
echo Build Normal edition...
hugo --environment chm
if not %errorlevel% == 0 exit /B 1

:: JP Help
hhc.exe public_chm\jp\project.hhp
:: hhc.exe returns 1 on success  https://ja.coder.work/so/cmd/1267049
if not %errorlevel% == 1 exit /B 1

:: EN Help
hhc.exe public_chm\en\project.hhp
if not %errorlevel% == 1 exit /B 1

:: -----------------------
:: Other Edition
:: -----------------------
echo Build some directory ignored edition...
hugo --environment chm_other_version
if not %errorlevel% == 0 exit /B 1

:: JP Help
hhc.exe public_chm_other_version\jp\project.hhp
if not %errorlevel% == 1 exit /B 1

:: EN Help
hhc.exe public_chm_other_version\en\project.hhp
if not %errorlevel% == 1 exit /B 1

popd
