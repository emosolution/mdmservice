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
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Microsoft.Extensions.Caching.Distributed;

namespace DMSpro.OMS.MdmService.ItemImages
{

    [Authorize(MdmServicePermissions.Items.Default)]
    public partial class ItemImagesAppService
    {
        public virtual async Task<PagedResultDto<ItemImageWithNavigationPropertiesDto>> GetListAsync(GetItemImagesInput input)
        {
            var totalCount = await _itemImageRepository.GetCountAsync(input.FilterText, input.Description, input.Active, input.DisplayOrderMin, input.DisplayOrderMax, input.FileId, input.ItemId);
            var items = await _itemImageRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Description, input.Active, input.DisplayOrderMin, input.DisplayOrderMax, input.FileId, input.ItemId, input.Sorting, input.MaxResultCount, input.SkipCount);

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

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemImageExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var itemImages = await _itemImageRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Description, input.Active, input.DisplayOrderMin, input.DisplayOrderMax, input.FileId);
            var items = itemImages.Select(item => new
            {
                Description = item.ItemImage.Description,
                Active = item.ItemImage.Active,
                DisplayOrder = item.ItemImage.DisplayOrder,
                FileId = item.ItemImage.FileId,

                ItemCode = item.Item?.Code,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
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