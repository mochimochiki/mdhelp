@echo off
pushd %~dp0\exampleSite

:: Build chm
call .\CI\build.bat chm_github_pages
if not %errorlevel% == 0 exit /B 1

:: copy to static
copy /y .\public_chm_github_pages\jp\mdhelpDocs.chm ..\static\mdhelpDocs.chm
if not %errorlevel% == 0 exit /B 1

:: Build docs
hugo --environment github_pages
if not %errorlevel% == 0 exit /B 1

popd
