@ECHO off

REM - don't waste time if already set up
IF DEFINED NOAHBOT_OUT (GOTO skip)

ECHO.
ECHO ----------------------
ECHO setting up build environment
ECHO ----------------------
ECHO.

REM - get user environment
CALL util\setup_env.bat
IF ERRORLEVEL 1 GOTO fail

REM - user environment check
IF NOT DEFINED NOAHBOT_HOME (GOTO envmissing)
IF NOT DEFINED NOAHBOT_DOTNET (GOTO envmissing)

REM - set, ensure output dir
SET NOAHBOT_OUT=%NOAHBOT_HOME%\bin
IF NOT EXIST %NOAHBOT_OUT% (MKDIR %NOAHBOT_OUT%)

REM - set compiler
SET NOAHBOT_CSC=%NOAHBOT_DOTNET%\csc.exe

REM - set up lib dependencies
SET NOAHBOT_DSHARP=%NOAHBOT_HOME%\lib\DSharpPlus.3.2.3\lib\net47
SET NOAHBOT_JSONNET=%NOAHBOT_HOME%\lib\Newtonsoft.Json.10.0.3\lib\net45

SET NOAHBOT_DSHARP_DLL=%NOAHBOT_DSHARP%\DSharpPlus.dll
SET NOAHBOT_JSONNET_DLL=%NOAHBOT_JSONNET%\Newtonsoft.Json.dll

ECHO done
GOTO :EOF

:envmissing
ECHO One or more environment variables aren't set -- did you add the right paths to setup_env?
ECHO NOAHBOT_HOME is %NOAHBOT_HOME%
ECHO NOAHBOT_DOTNET is %NOAHBOT_DOTNET%
GOTO fail

:fail
EXIT /b 1

:skip
ECHO.
ECHO environment already seems prepared - skipping setup
EXIT /b 0