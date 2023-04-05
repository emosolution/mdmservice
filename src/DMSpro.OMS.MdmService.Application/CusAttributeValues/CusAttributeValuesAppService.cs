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
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CusAttributeValues
{

    [Authorize(MdmServicePermissions.CustomerAttributes.Default)]
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

        [Authorize(MdmServicePermissions.CustomerAttributes.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            AllowEditDelete(id, "D");

            await _cusAttributeValueRepository.DeleteAsync(id);
        }

        private async void AllowEditDelete(Guid id, string action)
        {
            var attrValue = await _cusAttributeValueRepository.GetAsync(id);
            var attribute = await _customerAttributeRepository.GetAsync(attrValue.CustomerAttributeId);
            if (await _customerRepository.AnyAsync(x =>
                    (attribute.AttrNo == 0 && x.Attr0Id == id) ||
                    (attribute.AttrNo == 1 && x.Attr1Id == id) ||
                    (attribute.AttrNo == 2 && x.Attr2Id == id) ||
                    (attribute.AttrNo == 3 && x.Attr3Id == id) ||
                    (attribute.AttrNo == 4 && x.Attr4Id == id) ||
                    (attribute.AttrNo == 5 && x.Attr5Id == id) ||
                    (attribute.AttrNo == 6 && x.Attr6Id == id) ||
                    (attribute.AttrNo == 7 && x.Attr7Id == id) ||
                    (attribute.AttrNo == 8 && x.Attr8Id == id) ||
                    (attribute.AttrNo == 9 && x.Attr9Id == id) ||
                    (attribute.AttrNo == 10 && x.Attr10Id == id) ||
                    (attribute.AttrNo == 11 && x.Attr11Id == id) ||
                    (attribute.AttrNo == 12 && x.Attr12Id == id) ||
                    (attribute.AttrNo == 13 && x.Attr13Id == id) ||
                    (attribute.AttrNo == 14 && x.Attr14Id == id) ||
                    (attribute.AttrNo == 15 && x.Attr15Id == id) ||
                    (attribute.AttrNo == 16 && x.Attr16Id == id) ||
                    (attribute.AttrNo == 17 && x.Attr17Id == id) ||
                    (attribute.AttrNo == 18 && x.Attr18Id == id) ||
                    (attribute.AttrNo == 19 && x.Attr19Id == id)
                    ))
            {
                if (action == "D")
                    throw new UserFriendlyException(L["Error:General:DeleteContraint:550"]);
                if (action == "U")
                    throw new UserFriendlyException(L["Error:General:UpdateContraint:550"]);
            }
        }

        [Authorize(MdmServicePermissions.CustomerAttributes.Create)]
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

        [Authorize(MdmServicePermissions.CustomerAttributes.Edit)]
        public virtual async Task<CusAttributeValueDto> UpdateAsync(Guid id, CusAttributeValueUpdateDto input)
        {
            if (input.CustomerAttributeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["CustomerAttribute"]]);
            }

            AllowEditDelete(id, "U");

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