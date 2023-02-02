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
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{

    [Authorize(MdmServicePermissions.ItemAttributeValues.Default)]
    public partial class ItemAttributeValuesAppService : ApplicationService, IItemAttributeValuesAppService
    {
        private readonly IDistributedCache<ItemAttributeValueExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IItemAttributeValueRepository _itemAttributeValueRepository;
        private readonly ItemAttributeValueManager _itemAttributeValueManager;
        private readonly IRepository<ItemAttribute, Guid> _itemAttributeRepository;

        public ItemAttributeValuesAppService(IItemAttributeValueRepository itemAttributeValueRepository, ItemAttributeValueManager itemAttributeValueManager, IDistributedCache<ItemAttributeValueExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<ItemAttribute, Guid> itemAttributeRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _itemAttributeValueRepository = itemAttributeValueRepository;
            _itemAttributeValueManager = itemAttributeValueManager; _itemAttributeRepository = itemAttributeRepository;
        }

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