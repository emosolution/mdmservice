using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public partial class NumberingConfigDetailsAppService
    {
        [Authorize(MdmServicePermissions.NumberingConfigs.CreateDetail)]
        public virtual async Task<NumberingConfigDetailDto> CreateAsync(NumberingConfigDetailCreateDto input)
        {
            if (input.NumberingConfigId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["NumberingConfig"]]);
            }
            if (input.CompanyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Company"]]);
            }
            var detail = await _numberingConfigDetailManager.CreateAsync(input.Prefix, 
                input.PaddingZeroNumber, input.Suffix, input.Active, input.CurrentNumber,
                input.NumberingConfigId, input.CompanyId);

            return ObjectMapper.Map<NumberingConfigDetail, NumberingConfigDetailDto>(detail);
        }

        [Authorize(MdmServicePermissions.NumberingConfigs.Edit)]
        public virtual async Task<NumberingConfigDetailDto> UpdateAsync(Guid id,
            NumberingConfigDetailUpdateDto input)
        {
            var detail = await _numberingConfigDetailRepository.GetAsync(id);
            var header = await _numberingConfigRepository.GetAsync(detail.NumberingConfigId);
            var systemData = await _systemDataRepository.GetAsync(header.SystemDataId);

            (string prefix, int paddingZeroNumber, string suffix, int currentNumer, bool active) =
                NumberingConfigDetailConsts.GetBaseDetailData(input.Suffix,
                input.PaddingZeroNumber, input.Suffix, input.CurrentNumber,
                input.Active, systemData.ValueName);
            detail.Suffix = suffix;
            detail.PaddingZeroNumber = paddingZeroNumber;
            detail.Prefix = prefix;
            detail.CurrentNumber = currentNumer;
            detail.Active = active;
            await _numberingConfigDetailRepository.UpdateAsync(detail);

            return ObjectMapper.Map<NumberingConfigDetail, NumberingConfigDetailDto>(detail);
        }
    }
}
