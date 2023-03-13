using DMSpro.OMS.MdmService.NumberingConfigs;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
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

        private async Task<(NumberingConfigDetail, NumberingConfig)> GetDetailFromObjectTypeAndCompany(
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
            var header = headers.First();
            var details = await
                _numberingConfigDetailRepository.GetListAsync(x => x.CompanyId == companyId &&
                    x.NumberingConfigId == header.Id);
            if (details.Count > 1)
            {
                throw new BusinessException(message: L["Error:NumberingConfigDetail:550"], code: "1");
            }
            if (details.Count == 1)
            {
                return (details.First(), header);
            }
            return (null, header);
        }

        [Authorize(MdmServicePermissions.NumberingConfigs.Default)]
        public virtual async Task<NumberingConfigDetailDto> GetSuggestedNumberingConfigAsync(
            string objectType, Guid companyId)
        {
            (NumberingConfigDetail detail, NumberingConfig header) =
                await GetDetailFromObjectTypeAndCompany(objectType, companyId);
            if (detail != null)
            {
                if (detail.Active)
                {
                    return ObjectMapper.Map<NumberingConfigDetail, NumberingConfigDetailDto>(detail);
                }
                NumberingConfigDetailDto inactiveDto = new()
                {
                    Prefix = header.Prefix,
                    PaddingZeroNumber = header.PaddingZeroNumber,
                    Suffix = header.Suffix,
                    CurrentNumber = detail.CurrentNumber,
                    Active = false,
                    NumberingConfigId = header.Id,
                    CompanyId = companyId,
                };
                return inactiveDto;
            }
            NumberingConfigDetailDto dto = new()
            {
                Prefix = header.Prefix,
                PaddingZeroNumber = header.PaddingZeroNumber,
                Suffix = header.Suffix,
                CurrentNumber = NumberingConfigDetailConsts.CurrentNumberMinValue,
                Active = true,
                NumberingConfigId = header.Id,
                CompanyId = companyId,
            };
            return dto;
        }

        [Authorize(MdmServicePermissions.NumberingConfigs.Edit)]
        public virtual async Task<NumberingConfigDetailDto> SaveNumberingConfigAsync(
            string objectType, Guid companyId, int currentNumber)
        {
            (NumberingConfigDetail detail, NumberingConfig header) =
                await GetDetailFromObjectTypeAndCompany(objectType, companyId);
            var newCurrentNumber = currentNumber + 1;
            if (detail != null)
            {
                detail.CurrentNumber = newCurrentNumber;
                await _numberingConfigDetailRepository.UpdateAsync(detail);
                return ObjectMapper.Map<NumberingConfigDetail, NumberingConfigDetailDto>(detail);
            }
            var createDto = new NumberingConfigDetailCreateDto()
            {
                Prefix = header.Prefix,
                PaddingZeroNumber = header.PaddingZeroNumber,
                Suffix = header.Suffix,
                Active = true,
                CurrentNumber = newCurrentNumber,
                NumberingConfigId = header.Id,
                CompanyId = companyId,
            };
            var newDetail = await CreateAsync(createDto);
            return newDetail;
        }
    }
}
