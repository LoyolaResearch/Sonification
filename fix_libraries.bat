@echo off



:: Getting here means that we must reinvoke with elevation.
:: Add -Wait before -Verb RunAs to wait for the reinvocation to exit.
set ELEVATE_CMDLINE=cd /d "%~dp0" ^& "%~dp0makelinksignore.bat" %*
::echo %ELEVATE_CMDLINE%

powershell.exe -noprofile -c Start-Process -Wait -Verb RunAs cmd.exe \"/k $env:ELEVATE_CMDLINE\"
exit /b %ERRORLEVEL%
