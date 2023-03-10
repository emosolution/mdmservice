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
            Check.Length(input.Prefix, nameof(input.Prefix), NumberingConfigConsts.PrefixMaxLength);
            Check.Length(input.Suffix, nameof(input.Suffix), NumberingConfigConsts.SuffixMaxLength);
            Check.Range(input.PaddingZeroNumber,
                nameof(input.PaddingZeroNumber), NumberingConfigConsts.PaddingZeroNumberMinValue);

            var numberingConfig = await _numberingConfigRepository.GetAsync(id);

            numberingConfig.Prefix = input.Prefix;
            numberingConfig.Suffix = input.Suffix;
            numberingConfig.PaddingZeroNumber = input.PaddingZeroNumber;

            numberingConfig.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
            await _numberingConfigRepository.UpdateAsync(numberingConfig);

            return ObjectMapper.Map<NumberingConfig, NumberingConfigDto>(numberingConfig);
        }
    }
}