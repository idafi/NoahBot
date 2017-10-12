@ECHO off

REM - don't waste time if already set up
IF DEFINED CPU (
	IF "%1" == "" (GOTO skip)
	IF "%CPU%" == "%1" (GOTO skip)
)

ECHO.
ECHO ----------------------
ECHO setting up build environment
ECHO ----------------------
ECHO.

REM - get target cpu
IF "%1" == "" (SET CPU=x64) ELSE (SET CPU=%1)
IF NOT %CPU% == x64 IF NOT %CPU% == x86 GOTO badcpu

ECHO targeting win %CPU%

REM - get user environment
CALL util\setup_env.bat
IF ERRORLEVEL 1 GOTO fail

REM - user environment check
IF NOT DEFINED NOAHBOT_HOME (GOTO envmissing)
IF NOT DEFINED NOAHBOT_DOTNET (GOTO envmissing)

REM - set output dir
SET NOAHBOT_OUT=%NOAHBOT_HOME%\bin\%CPU%

REM - set compiler
SET NOAHBOT_CSC=%NOAHBOT_DOTNET%\csc.exe

REM - set up lib dependencies
SET NOAHBOT_DSHARP=%NOAHBOT_HOME%\lib\DSharpPlus.1.7.4\lib\net452
SET NOAHBOT_JSONNET=%NOAHBOT_HOME%\lib\Newtonsoft.Json.10.0.2\lib\net45
SET NOAHBOT_NUNIT=%NOAHBOT_HOME%\lib\NUnit.Framework-3.8.1\lib\net-4.5

SET NOAHBOT_DSHARP_DLL=%NOAHBOT_DSHARP%\DSharpPlus.dll
SET NOAHBOT_JSONNET_DLL=%NOAHBOT_JSONNET%\Newtonsoft.Json.dll
SET NOAHBOT_NUNIT_DLL=%NOAHBOT_NUNIT%\nunitlite.dll

ECHO done
GOTO :EOF

:envmissing
ECHO One or more environment variables aren't set -- did you add the right paths to setup_env?
ECHO NOAHBOT_HOME is %NOAHBOT_HOME%
ECHO NOAHBOT_DOTNET is %NOAHBOT_DOTNET%
GOTO fail

:badcpu
ECHO invalid cpu target specified ('%1'); expected x64 or x86
GOTO fail

:fail
EXIT /b 1

:skip
ECHO.
ECHO already targeting win %CPU% - skipping environment setup
EXIT /b 0