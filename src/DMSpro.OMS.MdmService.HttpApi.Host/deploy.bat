

set projectRoot=\Users\lecaothuy\Projects\redantsolution\
set microServiceFullName=MdmService
set commondllName=mdm
set microServicePath=src\DMSpro.OMS.%microServiceFullName%.
set microServiceFullPath=%projectRoot%%microServiceFullName%\%microServicePath%
set commondllPath=..\commondll\Development\Services\%commondllName%\
set deployedPath=\Users\lecaothuy\Projects\redantsolution\deploydmspro\
set deployFolder=MDMService
set microServiceDeployFolder=services\%deployFolder%\
set fullDeployPath=%deployedPath%%microServiceDeployFolder%
set commiter=kalickvn@gmail.com
set gitCommitMessage="MDM Deploy %microServiceFullName% by %commiter%"

dotnet clean

dotnet build

cd ..\..

copy %microServicePath%Application.Contracts\bin\Debug\netstandard2.0\*.Application.Contracts.dll %commondllPath%

copy %microServicePath%Domain.Shared\bin\Debug\netstandard2.0\*.Domain.Shared.dll %commondllPath%

copy %microServicePath%Domain\bin\Debug\netstandard2.0\*.Domain.dll %commondllPath%

copy %microServicePath%EntityFrameworkCore\bin\Debug\net7.0\*.EntityFrameworkCore.dll %commondllPath%

copy %microServicePath%HttpApi.Client\bin\Debug\netstandard2.0\*.HttpApi.Client.dll %commondllPath%

copy %microServicePath%Web\bin\Debug\net7.0\*.Web.dll %commondllPath%

cd %fullDeployPath%

cd ..

RD "%deployFolder%" /S /Q

mkdir %deployFolder%

cd %microServiceFullPath%HttpApi.Host

dotnet publish -c Release -o %fullDeployPath%

cd %fullDeployPath%

git add .

git commit -m %gitCommitMessage%

git push

cd %microServiceFullPath%HttpApi.Host

