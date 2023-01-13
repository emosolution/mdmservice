using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Items;
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
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.MdmService.ItemAttributes;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DMSpro.OMS.Shared.Lib.Parser;

namespace DMSpro.OMS.MdmService.ItemImages
{

    [Authorize(MdmServicePermissions.Items.Default)]
    public class ItemImagesAppService : ApplicationService, IItemImagesAppService
    {
        private readonly IDistributedCache<ItemImageExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IItemImageRepository _itemImageRepository;
        private readonly ItemImageManager _itemImageManager;
        private readonly IRepository<Item, Guid> _itemRepository;

        public ItemImagesAppService(IItemImageRepository itemImageRepository, ItemImageManager itemImageManager, IDistributedCache<ItemImageExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Item, Guid> itemRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _itemImageRepository = itemImageRepository;
            _itemImageManager = itemImageManager; _itemRepository = itemRepository;
        }

        public virtual async Task<PagedResultDto<ItemImageWithNavigationPropertiesDto>> GetListAsync(GetItemImagesInput input)
        {
            var totalCount = await _itemImageRepository.GetCountAsync(input.FilterText, input.Description, input.Url, input.Active, input.DisplayOrderMin, input.DisplayOrderMax, input.ItemId);
            var items = await _itemImageRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Description, input.Url, input.Active, input.DisplayOrderMin, input.DisplayOrderMax, input.ItemId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ItemImageWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ItemImageWithNavigationProperties>, List<ItemImageWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<ItemImageWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<ItemImageWithNavigationProperties, ItemImageWithNavigationPropertiesDto>
                (await _itemImageRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            var items = await _itemImageRepository.GetQueryableAsync();
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption, inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);
            results.data = ObjectMapper.Map<IEnumerable<ItemAttribute>, IEnumerable<ItemAttributeDto>>(results.data.Cast<ItemAttribute>());

            return results;

        }

        public virtual async Task<ItemImageDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ItemImage, ItemImageDto>(await _itemImageRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetItemLookupAsync(LookupRequestDto input)
        {
            var query = (await _itemRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Item>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Item>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.Items.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _itemImageRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.Items.Create)]
        public virtual async Task<ItemImageDto> CreateAsync(ItemImageCreateDto input)
        {
            if (input.ItemId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Item"]]);
            }

            var itemImage = await _itemImageManager.CreateAsync(
            input.ItemId, input.Description, input.Url, input.Active, input.DisplayOrder
            );

            return ObjectMapper.Map<ItemImage, ItemImageDto>(itemImage);
        }

        [Authorize(MdmServicePermissions.Items.Edit)]
        public virtual async Task<ItemImageDto> UpdateAsync(Guid id, ItemImageUpdateDto input)
        {
            if (input.ItemId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Item"]]);
            }

            var itemImage = await _itemImageManager.UpdateAsync(
            id,
            input.ItemId, input.Description, input.Url, input.Active, input.DisplayOrder, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<ItemImage, ItemImageDto>(itemImage);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemImageExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _itemImageRepository.GetListAsync(input.FilterText, input.Description, input.Url, input.Active, input.DisplayOrderMin, input.DisplayOrderMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<ItemImage>, List<ItemImageExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "ItemImages.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ItemImageExcelDownloadTokenCacheItem { Token = token },
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