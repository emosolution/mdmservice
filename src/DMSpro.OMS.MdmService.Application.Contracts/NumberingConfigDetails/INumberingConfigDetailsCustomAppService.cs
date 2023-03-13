using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public partial interface INumberingConfigDetailsAppService
    {
        Task<NumberingConfigDetailDto> GetSuggestedNumberingConfigAsync(string objectType, Guid companyId);

        Task<NumberingConfigDetailDto> SaveNumberingConfigAsync(string objectType, Guid companyId, int currentNumber);
    }
}