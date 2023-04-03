using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.NumberingConfigs;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Controllers.NumberingConfigs
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("NumberingConfigInternal")]
    [Route("api/mdm-service/numbering-configs-internal")]
    public partial class NumberingConfigInternalController : AbpController, INumberingConfigsInternalAppService
    {
        private readonly INumberingConfigsInternalAppService _appService;

        public NumberingConfigInternalController(INumberingConfigsInternalAppService appService)
        {
            _appService = appService;
        }

        [HttpPost]
        [Route("create-all-for-host")]
        public Task<List<NumberingConfigDto>> CreateAllConfigsForHostAsync()
        {
            return _appService.CreateAllConfigsForHostAsync();
        }

        [HttpPost]
        [Route("create-all-for-tenant")]
        public Task<List<NumberingConfigDto>> CreateAllConfigsForTenantAsync(List<Guid> tenantIds)
        {
            return _appService.CreateAllConfigsForTenantAsync(tenantIds);
        }

        [HttpPost]
        public Task<NumberingConfigDto> CreateAsync(NumberingConfigCreateDto input)
        {
            throw new NotImplementedException();
        }
    }
}