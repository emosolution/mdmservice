using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.SystemDatas
{
    public partial interface ISystemDatasInternalAppService : IApplicationService
    {
        Task<SystemDataDto> GetSystemDataByCodeAndValueName(string code, string valueName);
        
        Task<List<SystemDataDto>> GetNumberingConfigsSystemData();

        Task<SystemDataDto> GetNumberConfigSystemDataByValueName(string valueName);

        Task CreateAllForTenantAsync(List<Guid> tenantIds);

        Task CreateAllForHostAsync();
    }
}
