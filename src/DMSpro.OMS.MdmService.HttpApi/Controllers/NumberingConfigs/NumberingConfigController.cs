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
        public virtual Task<NumberingConfigDto> GetAsync(Guid id)
        {
            return _numberingConfigsAppService.GetAsync(id);
            
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<NumberingConfigDto> UpdateAsync(Guid id, NumberingConfigUpdateDto input)
        {
            return _numberingConfigsAppService.UpdateAsync(id, input);
        }
    }
}