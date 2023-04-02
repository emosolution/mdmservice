using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.ItemAttributes;
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
using DMSpro.OMS.MdmService.CusAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributes;
using DMSpro.OMS.MdmService.Customers;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{

    [Authorize(MdmServicePermissions.ItemAttributeValues.Default)]
    public partial class ItemAttributeValuesAppService 
    {
        public virtual async Task<PagedResultDto<ItemAttributeValueWithNavigationPropertiesDto>> GetListAsync(GetItemAttributeValuesInput input)
        {
            var totalCount = await _itemAttributeValueRepository.GetCountAsync(input.FilterText, input.AttrValName, input.ItemAttributeId, input.ParentId);
            var items = await _itemAttributeValueRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.AttrValName, input.ItemAttributeId, input.ParentId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ItemAttributeValueWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ItemAttributeValueWithNavigationProperties>, List<ItemAttributeValueWithNavigationPropertiesDto>>(items)
            };
        }
        public virtual async Task<ItemAttributeValueWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<ItemAttributeValueWithNavigationProperties, ItemAttributeValueWithNavigationPropertiesDto>
                (await _itemAttributeValueRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<ItemAttributeValueDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ItemAttributeValue, ItemAttributeValueDto>(await _itemAttributeValueRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetItemAttributeLookupAsync(LookupRequestDto input)
        {
            var query = (await _itemAttributeRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.AttrName != null &&
                         x.AttrName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<ItemAttribute>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ItemAttribute>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetItemAttributeValueLookupAsync(LookupRequestDto input)
        {
            var query = (await _itemAttributeValueRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.AttrValName != null &&
                         x.AttrValName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<ItemAttributeValue>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ItemAttributeValue>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.ItemAttributeValues.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var attrValue = await _itemAttributeValueRepository.GetAsync(id);
            var attribute = await _itemAttributeRepository.GetAsync(attrValue.ItemAttributeId);

            if (await _itemRepository.AnyAsync(x =>
                    (attribute.AttrNo == 0 && x.Attr0Id.HasValue) ||
                    (attribute.AttrNo == 1 && x.Attr1Id.HasValue) ||
                    (attribute.AttrNo == 2 && x.Attr2Id.HasValue) ||
                    (attribute.AttrNo == 3 && x.Attr3Id.HasValue) ||
                    (attribute.AttrNo == 4 && x.Attr4Id.HasValue) ||
                    (attribute.AttrNo == 5 && x.Attr5Id.HasValue) ||
                    (attribute.AttrNo == 6 && x.Attr6Id.HasValue) ||
                    (attribute.AttrNo == 7 && x.Attr7Id.HasValue) ||
                    (attribute.AttrNo == 8 && x.Attr8Id.HasValue) ||
                    (attribute.AttrNo == 9 && x.Attr9Id.HasValue) ||
                    (attribute.AttrNo == 10 && x.Attr10Id.HasValue) ||
                    (attribute.AttrNo == 11 && x.Attr11Id.HasValue) ||
                    (attribute.AttrNo == 12 && x.Attr12Id.HasValue) ||
                    (attribute.AttrNo == 13 && x.Attr13Id.HasValue) ||
                    (attribute.AttrNo == 14 && x.Attr14Id.HasValue) ||
                    (attribute.AttrNo == 15 && x.Attr15Id.HasValue) ||
                    (attribute.AttrNo == 16 && x.Attr16Id.HasValue) ||
                    (attribute.AttrNo == 17 && x.Attr17Id.HasValue) ||
                    (attribute.AttrNo == 18 && x.Attr18Id.HasValue) ||
                    (attribute.AttrNo == 19 && x.Attr19Id.HasValue)
                    ))
            {
                throw new UserFriendlyException(L["Error:General:DeleteContraint:550"]);
            }

            await _itemAttributeValueRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.ItemAttributeValues.Create)]
        public virtual async Task<ItemAttributeValueDto> CreateAsync(ItemAttributeValueCreateDto input)
        {
            if (input.ItemAttributeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemAttribute"]]);
            }

            var itemAttributeValue = await _itemAttributeValueManager.CreateAsync(
            input.ItemAttributeId, input.ParentId, input.AttrValName
            );

            return ObjectMapper.Map<ItemAttributeValue, ItemAttributeValueDto>(itemAttributeValue);
        }

        [Authorize(MdmServicePermissions.ItemAttributeValues.Edit)]
        public virtual async Task<ItemAttributeValueDto> UpdateAsync(Guid id, ItemAttributeValueUpdateDto input)
        {
            if (input.ItemAttributeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemAttribute"]]);
            }

            var itemAttributeValue = await _itemAttributeValueManager.UpdateAsync(
            id,
            input.ItemAttributeId, input.ParentId, input.AttrValName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<ItemAttributeValue, ItemAttributeValueDto>(itemAttributeValue);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemAttributeValueExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _itemAttributeValueRepository.GetListAsync(input.FilterText, input.AttrValName);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<ItemAttributeValue>, List<ItemAttributeValueExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "ItemAttributeValues.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ItemAttributeValueExcelDownloadTokenCacheItem { Token = token },
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