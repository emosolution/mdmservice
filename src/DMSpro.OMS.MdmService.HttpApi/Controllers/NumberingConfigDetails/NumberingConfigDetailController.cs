using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.NumberingConfigDetails;

namespace DMSpro.OMS.MdmService.Controllers.NumberingConfigDetails
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("NumberingConfigDetail")]
    [Route("api/mdm-service/numbering-config-details")]
    public partial class NumberingConfigDetailController : AbpController, INumberingConfigDetailsAppService
    {
        private readonly INumberingConfigDetailsAppService _numberingConfigDetailsAppService;

        public NumberingConfigDetailController(INumberingConfigDetailsAppService numberingConfigDetailsAppService)
        {
            _numberingConfigDetailsAppService = numberingConfigDetailsAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<NumberingConfigDetailDto> GetAsync(Guid id)
        {
            return _numberingConfigDetailsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<NumberingConfigDetailDto> CreateAsync(NumberingConfigDetailCreateDto input)
        {
            return _numberingConfigDetailsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<NumberingConfigDetailDto> UpdateAsync(Guid id, NumberingConfigDetailUpdateDto input)
        {
             return _numberingConfigDetailsAppService.UpdateAsync(id, input);
        }
    }
}