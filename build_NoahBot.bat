@ECHO off

IF NOT DEFINED NOAHBOT_OUT (GOTO envmissing)

ECHO.
ECHO ----------------------
ECHO building NoahBot
ECHO ----------------------
ECHO.

%NOAHBOT_CSC% /nologo ^
	/r:%NOAHBOT_JSONNET_DLL% ^
	/r:%NOAHBOT_DSHARP_DLL% ^
	/platform:anycpu /t:exe ^
	/out:%NOAHBOT_OUT%/NoahBot.exe ^
	/recurse:%NOAHBOT_HOME%/src/NoahBot/*.cs

GOTO :EOF

:envmissing
ECHO.
ECHO build environment isn't set up - did you forget to call setup.bat?

EXIT /b 1