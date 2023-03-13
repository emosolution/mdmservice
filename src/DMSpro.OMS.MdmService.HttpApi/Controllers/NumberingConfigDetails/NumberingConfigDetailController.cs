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
            try
            {
                return _numberingConfigDetailsAppService.GetAsync(id);
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
        public virtual Task<NumberingConfigDetailDto> CreateAsync(NumberingConfigDetailCreateDto input)
        {
            try
            {
                return _numberingConfigDetailsAppService.CreateAsync(input);
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
        public virtual Task<NumberingConfigDetailDto> UpdateAsync(Guid id, NumberingConfigDetailUpdateDto input)
        {
            try
            {
                return _numberingConfigDetailsAppService.UpdateAsync(id, input);
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