using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{

    [Authorize(MdmServicePermissions.NumberingConfigs.Default)]
    public partial class NumberingConfigsAppService
    {
        public virtual async Task<NumberingConfigDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<NumberingConfig, NumberingConfigDto>(
                await _numberingConfigRepository.GetAsync(id));
        }
    }
}