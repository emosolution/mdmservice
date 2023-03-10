using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public interface INumberingConfigsInternalAppService :  IApplicationService
    {
        Task<NumberingConfigDto> Create(NumberingConfigCreateDto input);

        Task<List<NumberingConfigDto>> CreateAllConfigsForTenant(List<Guid> tenantIds);

        Task<List<NumberingConfigDto>> CreateAllConfigsForHost();
    }
}
