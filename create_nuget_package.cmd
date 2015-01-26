@echo off

rmdir NuGetPackage /s /q
mkdir NuGetPackage
mkdir NuGetPackage\Epicycle.Input-cs.0.1.2.0
mkdir NuGetPackage\Epicycle.Input-cs.0.1.2.0\lib

copy package.nuspec NuGetPackage\Epicycle.Input-cs.0.1.2.0\Epicycle.Input-cs.0.1.2.0.nuspec
copy README.md NuGetPackage\Epicycle.Input-cs.0.1.2.0\README.md
copy LICENSE NuGetPackage\Epicycle.Input-cs.0.1.2.0\LICENSE

xcopy bin\net35\Release\Epicycle.Input_cs.dll NuGetPackage\Epicycle.Input-cs.0.1.2.0\lib\net35\
xcopy bin\net35\Release\Epicycle.Input_cs.pdb NuGetPackage\Epicycle.Input-cs.0.1.2.0\lib\net35\
xcopy bin\net35\Release\Epicycle.Input_cs.xml NuGetPackage\Epicycle.Input-cs.0.1.2.0\lib\net35\
xcopy bin\net40\Release\Epicycle.Input_cs.dll NuGetPackage\Epicycle.Input-cs.0.1.2.0\lib\net40\
xcopy bin\net40\Release\Epicycle.Input_cs.pdb NuGetPackage\Epicycle.Input-cs.0.1.2.0\lib\net40\
xcopy bin\net40\Release\Epicycle.Input_cs.xml NuGetPackage\Epicycle.Input-cs.0.1.2.0\lib\net40\
xcopy bin\net45\Release\Epicycle.Input_cs.dll NuGetPackage\Epicycle.Input-cs.0.1.2.0\lib\net45\
xcopy bin\net45\Release\Epicycle.Input_cs.pdb NuGetPackage\Epicycle.Input-cs.0.1.2.0\lib\net45\
xcopy bin\net45\Release\Epicycle.Input_cs.xml NuGetPackage\Epicycle.Input-cs.0.1.2.0\lib\net45\

pause