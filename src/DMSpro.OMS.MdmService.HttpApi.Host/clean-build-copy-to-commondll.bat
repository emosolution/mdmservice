@echo off

set microServiceFullName=MdmService
set commondllName=mdm
set microServicePath=src\DMSpro.OMS.%microServiceFullName%.
set commondllPath=..\commondll\Development\Services\%commondllName%\

cd ..\..

dotnet clean

dotnet build

copy %microServicePath%Application.Contracts\bin\Debug\netstandard2.0\*.Application.Contracts.dll %commondllPath%

copy %microServicePath%Domain.Shared\bin\Debug\netstandard2.0\*.Domain.Shared.dll %commondllPath%

copy %microServicePath%Domain\bin\Debug\netstandard2.0\*.Domain.dll %commondllPath%

copy %microServicePath%EntityFrameworkCore\bin\Debug\net7.0\*.EntityFrameworkCore.dll %commondllPath%

copy %microServicePath%HttpApi.Client\bin\Debug\netstandard2.0\*.HttpApi.Client.dll %commondllPath%

copy %microServicePath%Web\bin\Debug\net7.0\*.Web.dll %commondllPath%

cd %microServicePath%HttpApi.Host