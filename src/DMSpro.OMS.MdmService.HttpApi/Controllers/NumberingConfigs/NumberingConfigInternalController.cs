﻿using DMSpro.OMS.MdmService.NumberingConfigs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace DMSpro.OMS.MdmService.Controllers.NumberingConfigs
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("NumberingConfigInternal")]
    [Route("api/mdm-service/numbering-configs-internal")]
    
    public partial class NumberingConfigInternalController : AbpController,
        INumberingConfigsInternalAppService
    {
        private readonly INumberingConfigsInternalAppService _internalAppService;

        public NumberingConfigInternalController(INumberingConfigsInternalAppService internalAppService)
        {
            _internalAppService = internalAppService;
        }

        [HttpPost]
        [Route("create-all-for-host")]
        public virtual async Task<List<NumberingConfigDto>> CreateAllConfigsForHostAsync()
        {
            try
            {
                return await _internalAppService.CreateAllConfigsForHostAsync();
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }

        [HttpPost]
        [Route("create-all-for-tenants")]
        public virtual async Task<List<NumberingConfigDto>> CreateAllConfigsForTenantAsync(List<Guid> tenantIds)
        {
            try
            {
                return await _internalAppService.CreateAllConfigsForTenantAsync(tenantIds);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }

        [HttpPost]
        public virtual async Task<NumberingConfigDto> CreateAsync(NumberingConfigCreateDto input)
        {
            try
            {
                return await _internalAppService.CreateAsync(input);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }
    }
}
