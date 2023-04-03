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
    [ControllerName("SystemDataInternal")]
    [Route("api/mdm-service/system-datas-internal")]
    public partial class SystemDataInternalController : AbpController, ISystemDatasInternalAppService
    {
        private readonly ISystemDatasInternalAppService _appService;

        public SystemDataInternalController(ISystemDatasInternalAppService appService)
        {
            _appService = appService;
        }

        [HttpPost]
        [Route("create-all-for-host")]
        public virtual async Task CreateAllForHostAsync()
        {
            await _appService.CreateAllForHostAsync();
        }

        [HttpPost]
        [Route("create-all-for-tenant")]
        public virtual async Task CreateAllForTenantAsync(List<Guid> tenantIds)
        {
            await _appService.CreateAllForTenantAsync(tenantIds);
        }

        [HttpGet]
        [Route("for-number-config-by-value-name")]
        public Task<SystemDataDto> GetNumberConfigSystemDataByValueName(string valueName)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("for-numbering-config")]
        public Task<List<SystemDataDto>> GetNumberingConfigsSystemData()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("by-code-and-value-name")]
        public Task<SystemDataDto> GetSystemDataByCodeAndValueName(string code, string valueName)
        {
            throw new NotImplementedException();
        }
    }
}