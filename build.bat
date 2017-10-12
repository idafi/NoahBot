@ECHO off

CALL util\setup.bat %*
IF ERRORLEVEL 1 GOTO fail

REM - ensure output dir
IF NOT EXIST %NOAHBOT_OUT% (MKDIR %NOAHBOT_OUT%)

CALL build_noahbot.bat
IF ERRORLEVEL 1 GOTO fail

ECHO.
ECHO build successful!
GOTO :EOF

:fail
ECHO.
ECHO build failed