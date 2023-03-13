using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.NumberingConfigs;

namespace DMSpro.OMS.MdmService.Controllers.NumberingConfigs
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("NumberingConfig")]
    [Route("api/mdm-service/numbering-configs")]
    public partial class NumberingConfigController : AbpController, INumberingConfigsAppService
    {
        private readonly INumberingConfigsAppService _numberingConfigsAppService;

        public NumberingConfigController(INumberingConfigsAppService numberingConfigsAppService)
        {
            _numberingConfigsAppService = numberingConfigsAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<NumberingConfigDto> GetAsync(Guid id)
        {
            try
            {
                return await _numberingConfigsAppService.GetAsync(id);
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

        [HttpPut]
        [Route("{id}")]
        public virtual async Task<NumberingConfigDto> UpdateAsync(Guid id, NumberingConfigUpdateDto input)
        {
            try
            {
                return await _numberingConfigsAppService.UpdateAsync(id, input);
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