using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.CustomerAttributes;
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

namespace DMSpro.OMS.MdmService.CusAttributeValues
{

    [Authorize(MdmServicePermissions.CusAttributeValues.Default)]
    public partial class CusAttributeValuesAppService 
    {
        public virtual async Task<PagedResultDto<CusAttributeValueWithNavigationPropertiesDto>> GetListAsync(GetCusAttributeValuesInput input)
        {
            var totalCount = await _cusAttributeValueRepository.GetCountAsync(input.FilterText, input.AttrValName, input.CustomerAttributeId, input.ParentCusAttributeValueId);
            var items = await _cusAttributeValueRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.AttrValName, input.CustomerAttributeId, input.ParentCusAttributeValueId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CusAttributeValueWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CusAttributeValueWithNavigationProperties>, List<CusAttributeValueWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CusAttributeValueWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CusAttributeValueWithNavigationProperties, CusAttributeValueWithNavigationPropertiesDto>
                (await _cusAttributeValueRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<CusAttributeValueDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CusAttributeValue, CusAttributeValueDto>(await _cusAttributeValueRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCustomerAttributeLookupAsync(LookupRequestDto input)
        {
            var query = (await _customerAttributeRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.AttrName != null &&
                         x.AttrName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<CustomerAttribute>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerAttribute>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetCusAttributeValueLookupAsync(LookupRequestDto input)
        {
            var query = (await _cusAttributeValueRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.AttrValName != null &&
                         x.AttrValName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<CusAttributeValue>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CusAttributeValue>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.CusAttributeValues.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _cusAttributeValueRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CusAttributeValues.Create)]
        public virtual async Task<CusAttributeValueDto> CreateAsync(CusAttributeValueCreateDto input)
        {
            if (input.CustomerAttributeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerAttribute"]]);
            }

            var cusAttributeValue = await _cusAttributeValueManager.CreateAsync(
            input.CustomerAttributeId, input.ParentCusAttributeValueId, input.AttrValName
            );

            return ObjectMapper.Map<CusAttributeValue, CusAttributeValueDto>(cusAttributeValue);
        }

        [Authorize(MdmServicePermissions.CusAttributeValues.Edit)]
        public virtual async Task<CusAttributeValueDto> UpdateAsync(Guid id, CusAttributeValueUpdateDto input)
        {
            if (input.CustomerAttributeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerAttribute"]]);
            }

            var cusAttributeValue = await _cusAttributeValueManager.UpdateAsync(
            id,
            input.CustomerAttributeId, input.ParentCusAttributeValueId, input.AttrValName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CusAttributeValue, CusAttributeValueDto>(cusAttributeValue);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CusAttributeValueExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _cusAttributeValueRepository.GetListAsync(input.FilterText, input.AttrValName);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<CusAttributeValue>, List<CusAttributeValueExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CusAttributeValues.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CusAttributeValueExcelDownloadTokenCacheItem { Token = token },
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