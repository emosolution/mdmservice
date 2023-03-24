using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public interface INumberingConfigDetailsInternalAppService : IApplicationService
    {
        Task<NumberingConfigDetailDto> GetSuggestedNumberingConfigAsync(string objectType, Guid companyId);

        Task<NumberingConfigDetailDto> SaveNumberingConfigAsync(string objectType, Guid companyId, int currentNumber);
    }
}