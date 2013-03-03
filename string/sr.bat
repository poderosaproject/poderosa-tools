@echo off
setlocal
if "%~1" == "" (
  echo Usage: %~nx0 path_to_stringresource.xml
  goto END
)
set XSLT=%~dp0..\xslt\bin\Release\xslt.exe
set TRANS=%~dp0sr.xslt
set SOURCE=%~1
set DESTDIR=%~dp1
set LANGS=ja

if not exist "%XSLT%" (
  echo xslt.exe not found.
  goto END
)
if not exist "%TRANS%" (
  echo sr.xslt not found.
  goto END
)

echo SOURCE FILE: %SOURCE%
echo OUTPUT DIR:  %DESTDIR%

echo Output: "%DESTDIR%strings.resx"
"%XSLT%" -Dlang=en -s "%TRANS%" -i "%SOURCE%" -o "%DESTDIR%strings.resx"

for %%L in (%LANGS%) do (
  echo Output: "%DESTDIR%strings_%%L.resx"
  "%XSLT%" -Dlang=%%L -s "%TRANS%" -i "%SOURCE%" -o "%DESTDIR%strings_%%L.resx"
)

:END
pause
