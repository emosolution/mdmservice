using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.SystemDatas;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Controllers.SystemDatas
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("SystemData")]
    [Route("api/mdm-service/internal-system-datas")]
    public partial class SystemDataInternalController : AbpController, ISystemDatasInternalAppService
    {
        private readonly ISystemDatasInternalAppService _appService;

        public virtual async Task CreateAllForHostAsync()
        {
            await _appService.CreateAllForHostAsync();
        }

        public virtual async Task CreateAllForTenantAsync(List<Guid> tenantIds)
        {
            await _appService.CreateAllForTenantAsync(tenantIds);
        }

        public Task<SystemDataDto> GetNumberConfigSystemDataByValueName(string valueName)
        {
            throw new NotImplementedException();
        }

        public Task<List<SystemDataDto>> GetNumberingConfigsSystemData()
        {
            throw new NotImplementedException();
        }

        public Task<SystemDataDto> GetSystemDataByCodeAndValueName(string code, string valueName)
        {
            throw new NotImplementedException();
        }
    }
}