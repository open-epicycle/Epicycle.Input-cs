@echo off

cd projects
msbuild Epicycle.Input.net35.sln /t:Clean,Build /p:Configuration=Debug
msbuild Epicycle.Input.net35.sln /t:Clean,Build /p:Configuration=Release
msbuild Epicycle.Input.net40.sln /t:Clean,Build /p:Configuration=Debug
msbuild Epicycle.Input.net40.sln /t:Clean,Build /p:Configuration=Release
msbuild Epicycle.Input.net45.sln /t:Clean,Build /p:Configuration=Debug
msbuild Epicycle.Input.net45.sln /t:Clean,Build /p:Configuration=Release

pause
