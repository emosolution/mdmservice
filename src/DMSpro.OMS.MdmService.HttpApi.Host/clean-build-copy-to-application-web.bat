cd ..\..

dotnet clean

dotnet build

copy src\DMSpro.OMS.MdmService.Application.Contracts\bin\Debug\netstandard2.0\*.Application.Contracts.dll ..\commondll\Development\Services\mdm\

copy src\DMSpro.OMS.MdmService.Domain.Shared\bin\Debug\netstandard2.0\*.Domain.Shared.dll ..\commondll\Development\Services\mdm\

copy src\DMSpro.OMS.MdmService.Domain\bin\Debug\netstandard2.0\*.Domain.dll ..\commondll\Development\Services\mdm\

copy src\DMSpro.OMS.MdmService.EntityFrameworkCore\bin\Debug\net7.0\*.EntityFrameworkCore.dll ..\commondll\Development\Services\mdm\

copy src\DMSpro.OMS.MdmService.HttpApi.Client\bin\Debug\netstandard2.0\*.HttpApi.Client.dll ..\commondll\Development\Services\mdm\

copy src\DMSpro.OMS.MdmService.Web\bin\Debug\net7.0\*.Web.dll ..\commondll\Development\Services\mdm\

cd src\DMSpro.OMS.MdmService.HttpApi.Host