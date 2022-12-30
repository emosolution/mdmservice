using DMSpro.OMS.MdmService.ItemMasters;
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
using DMSpro.OMS.MdmService.ItemAttachments;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;

using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Lib.Parser;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.ItemAttachments
{
    [RemoteService(IsEnabled = false)]
    [Authorize(MdmServicePermissions.ItemAttachments.Default)]
    public class ItemAttachmentsAppService : ApplicationService, IItemAttachmentsAppService
    {
        private readonly IDistributedCache<ItemAttachmentExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IItemAttachmentRepository _itemAttachmentRepository;
        private readonly ItemAttachmentManager _itemAttachmentManager;
        private readonly IRepository<ItemMaster, Guid> _itemMasterRepository;

        public ItemAttachmentsAppService(IItemAttachmentRepository itemAttachmentRepository, ItemAttachmentManager itemAttachmentManager, IDistributedCache<ItemAttachmentExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<ItemMaster, Guid> itemMasterRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _itemAttachmentRepository = itemAttachmentRepository;
            _itemAttachmentManager = itemAttachmentManager; _itemMasterRepository = itemMasterRepository;
        }

        public virtual async Task<PagedResultDto<ItemAttachmentWithNavigationPropertiesDto>> GetListAsync(GetItemAttachmentsInput input)
        {
            var totalCount = await _itemAttachmentRepository.GetCountAsync(input.FilterText, input.Description, input.Active, input.URL, input.ItemId);
            var items = await _itemAttachmentRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Description, input.Active, input.URL, input.ItemId, input.Sorting, input.MaxResultCount, input.SkipCount);

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

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _itemAttachmentRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<ItemAttachment>, IEnumerable<ItemAttachmentDto>>(results.data.Cast<ItemAttachment>());
            
            return results;
                
        }
        public virtual async Task<ItemAttachmentDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ItemAttachment, ItemAttachmentDto>(await _itemAttachmentRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetItemMasterLookupAsync(LookupRequestDto input)
        {
            var query = (await _itemMasterRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<ItemMaster>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ItemMaster>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.ItemAttachments.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _itemAttachmentRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.ItemAttachments.Create)]
        public virtual async Task<ItemAttachmentDto> CreateAsync(ItemAttachmentCreateDto input)
        {
            if (input.ItemId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemMaster"]]);
            }

            var itemAttachment = await _itemAttachmentManager.CreateAsync(
            input.ItemId, input.Description, input.Active, input.URL
            );

            return ObjectMapper.Map<ItemAttachment, ItemAttachmentDto>(itemAttachment);
        }

        [Authorize(MdmServicePermissions.ItemAttachments.Edit)]
        public virtual async Task<ItemAttachmentDto> UpdateAsync(Guid id, ItemAttachmentUpdateDto input)
        {
            if (input.ItemId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemMaster"]]);
            }

            var itemAttachment = await _itemAttachmentManager.UpdateAsync(
            id,
            input.ItemId, input.Description, input.Active, input.URL, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<ItemAttachment, ItemAttachmentDto>(itemAttachment);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemAttachmentExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _itemAttachmentRepository.GetListAsync(input.FilterText, input.Description, input.Active, input.URL);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<ItemAttachment>, List<ItemAttachmentExcelDto>>(items));
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
