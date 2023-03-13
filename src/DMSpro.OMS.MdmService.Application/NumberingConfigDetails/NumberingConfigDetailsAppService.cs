using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{

    [Authorize(MdmServicePermissions.NumberingConfigs.Default)]
    public partial class NumberingConfigDetailsAppService
    {
        public virtual async Task<NumberingConfigDetailDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<NumberingConfigDetail, NumberingConfigDetailDto>(await _numberingConfigDetailRepository.GetAsync(id));
        }
    }
}