using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;
using System.Threading.Tasks;
using Volo.Abp;
using System;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public partial class NumberingConfigsAppService
    {
        [Authorize(MdmServicePermissions.NumberingConfigs.Edit)]
        public virtual async Task<NumberingConfigDto> UpdateAsync(Guid id, NumberingConfigUpdateDto input)
        {
            var numberingConfig = await _numberingConfigRepository.GetAsync(id);
            var systemData = await _systemDataRepository.GetAsync(numberingConfig.SystemDataId);
            (string prefix, int paddingZeroNumber, string suffix) =
               NumberingConfigConsts.GetBaseData(input.Prefix,
                   input.PaddingZeroNumber, input.Suffix, systemData.ValueName);

            numberingConfig.Prefix = prefix;
            numberingConfig.Suffix = suffix;
            numberingConfig.PaddingZeroNumber = paddingZeroNumber;

            numberingConfig.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
            await _numberingConfigRepository.UpdateAsync(numberingConfig);

            return ObjectMapper.Map<NumberingConfig, NumberingConfigDto>(numberingConfig);
        }
    }
}