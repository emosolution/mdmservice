using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public partial interface INumberingConfigsAppService : IApplicationService
    {
        Task<NumberingConfigDto> GetAsync(Guid id);

        Task<NumberingConfigDto> UpdateAsync(Guid id, NumberingConfigUpdateDto input);
    }
}