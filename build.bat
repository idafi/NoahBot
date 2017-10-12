@ECHO off

CALL util\setup.bat %*
IF ERRORLEVEL 1 GOTO fail

REM - ensure output dir
IF NOT EXIST %NOAHBOT_OUT% (MKDIR %NOAHBOT_OUT%)

CALL build_noahbot.bat
IF ERRORLEVEL 1 GOTO fail

ECHO.
ECHO ----------------------
ECHO copying dependencies
ECHO ----------------------
ECHO.

XCOPY /Q /Y %NOAHBOT_DSHARP_DLL% %NOAHBOT_OUT%
IF ERRORLEVEL 1 GOTO fail

XCOPY /Q /Y %NOAHBOT_JSONNET_DLL% %NOAHBOT_OUT%
IF ERRORLEVEL 1 GOTO fail

ECHO done

ECHO.
ECHO build successful!
GOTO :EOF

:fail
ECHO.
ECHO build failed