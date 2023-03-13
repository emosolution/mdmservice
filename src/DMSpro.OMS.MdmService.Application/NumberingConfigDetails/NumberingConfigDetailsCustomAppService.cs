using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.ObjectMapping;

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
            var header = await _numberingConfigRepository.GetAsync(input.NumberingConfigId);
            var company = await _companyRepository.GetAsync(input.CompanyId);
            var systemData = await _systemDataRepository.GetAsync(header.SystemDataId);
            (string prefix, int paddingZeroNumber, string suffix, int currentNumer, bool active) =
                NumberingConfigDetailConsts.GetBaseDetailData(input.Suffix,
                input.PaddingZeroNumber, input.Suffix, input.CurrentNumber,
                input.Active, systemData.ValueName);
            string description = $"Numbering config detail for compamy ${company.Code}, type ${systemData.ValueName}";

            var numberingConfigDetail = new NumberingConfigDetail(
                GuidGenerator.Create(), input.NumberingConfigId, input.CompanyId,
                    description, prefix, paddingZeroNumber, suffix, active,
                    currentNumer);

            var detail = await _numberingConfigDetailRepository.InsertAsync(numberingConfigDetail);

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

        [Authorize(MdmServicePermissions.NumberingConfigs.CreateDetail)]
        public virtual async Task<NumberingConfigDetailDto> GetConfigDetailByObjectTypeAndCompanyAsync(
            string objectType, Guid companyId)
        {
            var company = _companyRepository.GetAsync(companyId);
            var systemData =
                await _systemDatasInternalAppService.GetNumberConfigSystemDataByValueName(objectType);
            var headers = await _numberingConfigRepository.GetListAsync(x =>
                x.SystemDataId == systemData.Id);
            if (headers.Count != 1)
            {
                throw new BusinessException(message: L["Error:NumberingConfig:550"], code: "1");
            }
            var details = await
                _numberingConfigDetailRepository.GetListAsync(x => x.CompanyId == companyId);
            if (details.Count > 1)
            {
                throw new BusinessException(message: L["Error:NumberingConfigDetail:550"], code: "1");
            }
            if (details.Count == 1)
            {
                var detail = details.First();
                return ObjectMapper.Map<NumberingConfigDetail, NumberingConfigDetailDto>(detail);
            }
            var header = headers.First();
            NumberingConfigDetailCreateDto input = new()
            {
                Prefix = header.Prefix,
                PaddingZeroNumber = header.PaddingZeroNumber,
                Suffix = header.Suffix,
                CurrentNumber = NumberingConfigDetailConsts.CurrentNumberMinValue,
                Active = true,
            };
            var dto = await CreateAsync(input);
            return dto;
    }
}
}
