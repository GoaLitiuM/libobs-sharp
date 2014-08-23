@echo off

::adjust these for custom install locations (64 for 64-bit windows, 32 for 32-bit windows)
SET MONOLOCATION64=%ProgramFiles(x86)%\Mono-3.2.3
SET MONOLOCATION32=%ProgramFiles%\Mono-3.2.3


::do not touch starting now
SET TARGETLOCATION64=%ProgramFiles(x86)%\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5
SET TARGETLOCATION32=%ProgramFiles%\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5
SET STARTLOC=%~dp0

IF "%PROCESSOR_ARCHITECTURE%" == "AMD64" GOTO 64bit
IF "%PROCESSOR_ARCHITECTURE%" == "x86" GOTO 32bit
echo Error: Unknown processor architecture: %PROCESSOR_ARCHITECTURE%
pause
goto end

:64bit
echo 64-bit Windows detected...
SET MONOLOCATION=%MONOLOCATION64%
SET TARGETLOCATION=%TARGETLOCATION64%
goto prepare

:32bit
echo 32-bit Windows detected...
SET MONOLOCATION=%MONOLOCATION32%
SET TARGETLOCATION=%TARGETLOCATION32%
goto prepare

pause

:prepare

cd /D "%TARGETLOCATION%"

mkdir Profile
cd Profile

echo Creating symbolic link to Mono assemblies...
mklink /d Mono "%MONOLOCATION%\lib\mono\4.5"

echo Copying RedistList...
cd /D "%STARTLOC%"
mkdir "%TARGETLOCATION%\Profile\Mono\RedistList"
copy /Y RedistList "%TARGETLOCATION%\Profile\Mono\RedistList\"

::fixed in 3.4.0, not available yet for Windows users
::https://bugzilla.xamarin.com/show_bug.cgi?id=8309
echo Applying libgdiplus mapping fix (3.2.3 only)...
cd /D "%STARTLOC%\libgdiplusfix"
copy /Y config "%MONOLOCATION%\etc\mono"

echo Setting up registry...
cd /D "%STARTLOC%"
regedit /s PrepareRegistry.reg

echo Done.

:end