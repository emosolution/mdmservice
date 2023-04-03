using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

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

            await CheckExistingConfigDetail(input.NumberingConfigId, input.CompanyId);

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

            await CheckExistingConfigDetail(header.Id, detail.CompanyId);

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

        private async Task CheckExistingConfigDetail(Guid numberingConfigId, Guid companyId)
        {
            if (await _numberingConfigDetailRepository.AnyAsync(
                x => x.NumberingConfigId == numberingConfigId &&
                x.CompanyId == companyId))
            {
                throw new UserFriendlyException(message: L["Error:NumberingConfigDetail:551"], code: "0");
            }
        }
    }
}
