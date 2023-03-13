using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public interface INumberingConfigsInternalAppService :  IApplicationService
    {
        Task<NumberingConfigDto> CreateAsync(NumberingConfigCreateDto input);

        Task<List<NumberingConfigDto>> CreateAllConfigsForTenantAsync(List<Guid> tenantIds);

        Task<List<NumberingConfigDto>> CreateAllConfigsForHostAsync();
    }
}
