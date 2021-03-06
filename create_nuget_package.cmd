@echo off

rmdir NuGetPackage /s /q
mkdir NuGetPackage
mkdir NuGetPackage\Epicycle.Input-cs.0.1.4.0
mkdir NuGetPackage\Epicycle.Input-cs.0.1.4.0\lib

copy package.nuspec NuGetPackage\Epicycle.Input-cs.0.1.4.0\Epicycle.Input-cs.0.1.4.0.nuspec
copy README.md NuGetPackage\Epicycle.Input-cs.0.1.4.0\README.md
copy LICENSE NuGetPackage\Epicycle.Input-cs.0.1.4.0\LICENSE

xcopy bin\net35\Release\Epicycle.Input_cs.dll NuGetPackage\Epicycle.Input-cs.0.1.4.0\lib\net35\
xcopy bin\net35\Release\Epicycle.Input_cs.pdb NuGetPackage\Epicycle.Input-cs.0.1.4.0\lib\net35\
xcopy bin\net35\Release\Epicycle.Input_cs.xml NuGetPackage\Epicycle.Input-cs.0.1.4.0\lib\net35\
xcopy bin\net40\Release\Epicycle.Input_cs.dll NuGetPackage\Epicycle.Input-cs.0.1.4.0\lib\net40\
xcopy bin\net40\Release\Epicycle.Input_cs.pdb NuGetPackage\Epicycle.Input-cs.0.1.4.0\lib\net40\
xcopy bin\net40\Release\Epicycle.Input_cs.xml NuGetPackage\Epicycle.Input-cs.0.1.4.0\lib\net40\
xcopy bin\net45\Release\Epicycle.Input_cs.dll NuGetPackage\Epicycle.Input-cs.0.1.4.0\lib\net45\
xcopy bin\net45\Release\Epicycle.Input_cs.pdb NuGetPackage\Epicycle.Input-cs.0.1.4.0\lib\net45\
xcopy bin\net45\Release\Epicycle.Input_cs.xml NuGetPackage\Epicycle.Input-cs.0.1.4.0\lib\net45\

cd NuGetPackage
nuget pack Epicycle.Input-cs.0.1.4.0\Epicycle.Input-cs.0.1.4.0.nuspec -Properties version=0.1.4.0
7z a -tzip Epicycle.Input-cs.0.1.4.0.zip Epicycle.Input-cs.0.1.4.0 Epicycle.Input-cs.0.1.4.0.nupkg

echo nuget push Epicycle.Input-cs.0.1.4.0.nupkg > push.cmd
echo pause >> push.cmd

pause