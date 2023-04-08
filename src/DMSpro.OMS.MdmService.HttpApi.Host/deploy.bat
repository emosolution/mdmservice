@echo off

set microServiceFullName=MdmService
set commondllName=mdm
set microServicePath=src\DMSpro.OMS.%microServiceFullName%.
set commondllPath=..\commondll\Development\Services\%commondllName%\
set deployedPath=C:\Code\redant\deploydmspro\
set microServiceDeployFolder=services\MDMService\
set fullDeployPath=%deployedPath%%microServiceDeployFolder%

dotnet clean

dotnet build

cd ..\..

copy %microServicePath%Application.Contracts\bin\Debug\netstandard2.0\*.Application.Contracts.dll %commondllPath%

copy %microServicePath%Domain.Shared\bin\Debug\netstandard2.0\*.Domain.Shared.dll %commondllPath%

copy %microServicePath%Domain\bin\Debug\netstandard2.0\*.Domain.dll %commondllPath%

copy %microServicePath%EntityFrameworkCore\bin\Debug\net7.0\*.EntityFrameworkCore.dll %commondllPath%

copy %microServicePath%HttpApi.Client\bin\Debug\netstandard2.0\*.HttpApi.Client.dll %commondllPath%

copy %microServicePath%Web\bin\Debug\net7.0\*.Web.dll %commondllPath%

dotnet publish -o %fullDeployPath%

cd %microServicePath%HttpApi.Host