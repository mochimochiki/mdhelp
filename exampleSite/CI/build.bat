@echo off
pushd %~dp0..

echo Build HUGO site...
hugo --environment chm

echo Compile chm help...
CI\workshop\hhc.exe public\jp\project.hhp

popd
