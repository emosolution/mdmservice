using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.NumberingConfigs;
using DMSpro.OMS.MdmService.SystemDatas;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public class NumberingConfigDetailsInternalAppService : ApplicationService, INumberingConfigDetailsInternalAppService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ISystemDatasInternalAppService _systemDatasInternalAppService;
        private readonly INumberingConfigRepository _numberingConfigRepository;
        private readonly INumberingConfigDetailRepository _numberingConfigDetailRepository;

        private readonly NumberingConfigDetailManager _numberingConfigDetailManager;

        public NumberingConfigDetailsInternalAppService(
            ICompanyRepository companyRepository,
            ISystemDatasInternalAppService systemDatasInternalAppService,
            INumberingConfigRepository numberingConfigRepository,
            INumberingConfigDetailRepository numberingConfigDetailRepository,
            NumberingConfigDetailManager numberingConfigDetailManager
            )
        {
            _companyRepository = companyRepository;
            _systemDatasInternalAppService = systemDatasInternalAppService;
            _numberingConfigRepository = numberingConfigRepository;
            _numberingConfigDetailRepository = numberingConfigDetailRepository;
            _numberingConfigDetailManager = numberingConfigDetailManager;
        }

        private async Task<(NumberingConfigDetail, NumberingConfig)> GetDetailFromObjectTypeAndCompany(
            string objectType, Guid companyId)
        {
            var company = await _companyRepository.GetAsync(companyId);
            var systemData =
                await _systemDatasInternalAppService.GetNumberConfigSystemDataByValueName(objectType);
            var headers = await _numberingConfigRepository.GetListAsync(x =>
                x.SystemDataId == systemData.Id);
            if (headers.Count != 1)
            {
                throw new BusinessException(message: L["Error:NumberingConfig:550"], code: "1");
            }
            var header = headers.First();
            var details =
                await _numberingConfigDetailRepository.GetListAsync(
                    x => x.CompanyId == companyId &&
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

        [AllowAnonymous]
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

        [AllowAnonymous]
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
            var newDetail = 
                await _numberingConfigDetailManager.CreateAsync(createDto.Prefix,
                    createDto.PaddingZeroNumber, createDto.Suffix, createDto.Active,
                    createDto.CurrentNumber, createDto.NumberingConfigId,
                    createDto.CompanyId);
            return ObjectMapper.Map<NumberingConfigDetail, NumberingConfigDetailDto>(newDetail);
        }
    }
}
