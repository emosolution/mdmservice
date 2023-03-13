using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public partial interface INumberingConfigDetailsAppService
    {
        Task<NumberingConfigDetailDto> GetConfigDetailByObjectTypeAndCompanyAsync(string objectType, Guid companyId);
    }
}