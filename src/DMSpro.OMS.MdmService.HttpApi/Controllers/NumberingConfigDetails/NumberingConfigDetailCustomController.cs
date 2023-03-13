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
        [Route("by-object-type-and-company")]
        public Task<NumberingConfigDetailDto> GetConfigDetailByObjectTypeAndCompanyAsync(string objectType, Guid companyId)
        {
            try
            {
                return _numberingConfigDetailsAppService.GetConfigDetailByObjectTypeAndCompanyAsync(objectType, companyId);
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