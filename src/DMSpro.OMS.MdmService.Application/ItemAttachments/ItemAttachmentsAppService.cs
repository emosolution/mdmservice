using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Items;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Microsoft.Extensions.Caching.Distributed;

namespace DMSpro.OMS.MdmService.ItemAttachments
{

    [Authorize(MdmServicePermissions.Items.Default)]
    public partial class ItemAttachmentsAppService 
    {
        public virtual async Task<PagedResultDto<ItemAttachmentWithNavigationPropertiesDto>> GetListAsync(GetItemAttachmentsInput input)
        {
            var totalCount = await _itemAttachmentRepository.GetCountAsync(input.FilterText, input.Description, input.Active, input.FileId, input.ItemId);
            var items = await _itemAttachmentRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Description, input.Active, input.FileId, input.ItemId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ItemAttachmentWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ItemAttachmentWithNavigationProperties>, List<ItemAttachmentWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<ItemAttachmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<ItemAttachmentWithNavigationProperties, ItemAttachmentWithNavigationPropertiesDto>
                (await _itemAttachmentRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<ItemAttachmentDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ItemAttachment, ItemAttachmentDto>(await _itemAttachmentRepository.GetAsync(id));
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
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemAttachmentExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var itemAttachments = await _itemAttachmentRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Description, input.Active, input.FileId);
            var items = itemAttachments.Select(item => new
            {
                Description = item.ItemAttachment.Description,
                Active = item.ItemAttachment.Active,
                FileId = item.ItemAttachment.FileId,

                ItemCode = item.Item?.Code,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "ItemAttachments.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ItemAttachmentExcelDownloadTokenCacheItem { Token = token },
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