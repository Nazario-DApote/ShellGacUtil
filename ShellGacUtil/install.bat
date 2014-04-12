@echo off
call "%VS100COMNTOOLS%\vsvars32.bat" x86
ECHO -----------------------------------------------------------------------
REM gacutil /i %1
ECHO -----------------------------------------------------------------------
regasm %1 /register /codebase
pause