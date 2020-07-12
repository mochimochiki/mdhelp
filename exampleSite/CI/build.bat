@echo off
pushd %~dp0..

echo Build HUGO site...
hugo --environment chm
hugo --environment chm_other_version

echo Compile chm help...
hhc.exe public_chm\jp\project.hhp
hhc.exe public_chm_other_version\jp\project.hhp

popd
