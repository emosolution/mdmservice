using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.NumberingConfigs;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Microsoft.Extensions.Caching.Distributed;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{

    [Authorize(MdmServicePermissions.NumberingConfigs.Default)]
    public partial class NumberingConfigDetailsAppService
    {
        public virtual async Task<PagedResultDto<NumberingConfigDetailWithNavigationPropertiesDto>> GetListAsync(GetNumberingConfigDetailsInput input)
        {
            var totalCount = await _numberingConfigDetailRepository.GetCountAsync(input.FilterText, input.Description, input.Prefix, input.PaddingZeroNumberMin, input.PaddingZeroNumberMax, input.Suffix, input.Active, input.CurrentNumberMin, input.CurrentNumberMax, input.NumberingConfigId, input.CompanyId);
            var items = await _numberingConfigDetailRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Description, input.Prefix, input.PaddingZeroNumberMin, input.PaddingZeroNumberMax, input.Suffix, input.Active, input.CurrentNumberMin, input.CurrentNumberMax, input.NumberingConfigId, input.CompanyId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<NumberingConfigDetailWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<NumberingConfigDetailWithNavigationProperties>, List<NumberingConfigDetailWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<NumberingConfigDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<NumberingConfigDetailWithNavigationProperties, NumberingConfigDetailWithNavigationPropertiesDto>
                (await _numberingConfigDetailRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<NumberingConfigDetailDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<NumberingConfigDetail, NumberingConfigDetailDto>(await _numberingConfigDetailRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetNumberingConfigLookupAsync(LookupRequestDto input)
        {
            var query = (await _numberingConfigRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Description != null &&
                         x.Description.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<NumberingConfig>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<NumberingConfig>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            var query = (await _companyRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Company>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Company>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await _numberingConfigDetailRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.MasterDataManipulators.CreateNumberConfigs)]
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

            var numberingConfigDetail = await _numberingConfigDetailManager.CreateAsync(
            input.NumberingConfigId, input.CompanyId, input.Description, input.Prefix, input.PaddingZeroNumber, input.Suffix, input.Active, input.CurrentNumber
            );

            return ObjectMapper.Map<NumberingConfigDetail, NumberingConfigDetailDto>(numberingConfigDetail);
        }

        [Authorize(MdmServicePermissions.NumberingConfigs.Edit)]
        public virtual async Task<NumberingConfigDetailDto> UpdateAsync(Guid id, NumberingConfigDetailUpdateDto input)
        {
            if (input.NumberingConfigId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["NumberingConfig"]]);
            }
            if (input.CompanyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Company"]]);
            }

            var numberingConfigDetail = await _numberingConfigDetailManager.UpdateAsync(
            id,
            input.NumberingConfigId, input.CompanyId, input.Description, input.Prefix, input.PaddingZeroNumber, input.Suffix, input.Active, input.CurrentNumber, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<NumberingConfigDetail, NumberingConfigDetailDto>(numberingConfigDetail);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(NumberingConfigDetailExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var numberingConfigDetails = await _numberingConfigDetailRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Description, input.Prefix, input.PaddingZeroNumberMin, input.PaddingZeroNumberMax, input.Suffix, input.Active, input.CurrentNumberMin, input.CurrentNumberMax);
            var items = numberingConfigDetails.Select(item => new
            {
                Description = item.NumberingConfigDetail.Description,
                Prefix = item.NumberingConfigDetail.Prefix,
                PaddingZeroNumber = item.NumberingConfigDetail.PaddingZeroNumber,
                Suffix = item.NumberingConfigDetail.Suffix,
                Active = item.NumberingConfigDetail.Active,
                CurrentNumber = item.NumberingConfigDetail.CurrentNumber,

                NumberingConfigDescription = item.NumberingConfig?.Description,
                CompanyCode = item.Company?.Code,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "NumberingConfigDetails.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new NumberingConfigDetailExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}