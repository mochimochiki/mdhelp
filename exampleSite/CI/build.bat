@echo off
pushd %~dp0..

echo Build Normal edition...
hugo --environment chm
if not %errorlevel% == 0 exit /B 1

hhc.exe public_chm\jp\project.hhp
if not %errorlevel% == 1 exit /B 1
:: hhc.exe returns 1 on success  https://ja.coder.work/so/cmd/1267049

echo Build some directory ignored edition...
hugo --environment chm_other_version
if not %errorlevel% == 0 exit /B 1

hhc.exe public_chm_other_version\jp\project.hhp
if not %errorlevel% == 1 exit /B 1
:: hhc.exe returns 1 on success  https://ja.coder.work/so/cmd/1267049

popd
