using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using DMSpro.OMS.MdmService.NumberingConfigDetails;

namespace DMSpro.OMS.MdmService.Controllers.NumberingConfigDetails
{
    public partial class NumberingConfigDetailController
    {
        [HttpGet]
        [Route("suggested-numbering")]
        public Task<NumberingConfigDetailDto> GetSuggestedNumberingConfigAsync(string objectType, Guid companyId)
        {
            try
            {
                return _numberingConfigDetailsAppService.GetSuggestedNumberingConfigAsync(objectType, companyId);
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
        [Route("save-numbering")]
        public Task<NumberingConfigDetailDto> SaveNumberingConfigAsync(string objectType, Guid companyId, int currentNumber)
        {
            try
            {
                return _numberingConfigDetailsAppService.SaveNumberingConfigAsync(objectType, companyId, currentNumber);
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