using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.Items;
using DMSpro.OMS.MdmService.FileManagementInfo;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    [Authorize(MdmServicePermissions.Items.Default)]
    public partial class ItemAttachmentsAppService : PartialAppService<ItemAttachment, ItemAttachmentDto, IItemAttachmentRepository>,
        IItemAttachmentsAppService
    {
        private readonly IItemAttachmentRepository _itemAttachmentRepository;
        private readonly IDistributedCache<ItemAttachmentExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly ItemAttachmentManager _itemAttachmentManager;
        private readonly IFileManagementInfoAppService _fileManagementInfoAppService;

        private readonly IItemRepository _itemRepository;

        public ItemAttachmentsAppService(ICurrentTenant currentTenant,
            IItemAttachmentRepository repository,
            ItemAttachmentManager itemAttachmentManager,
            IFileManagementInfoAppService fileManagementInfoAppService,
            IConfiguration settingProvider,
            IItemRepository itemRepository,
            IDistributedCache<ItemAttachmentExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _itemAttachmentRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _itemAttachmentManager = itemAttachmentManager;
            _fileManagementInfoAppService = fileManagementInfoAppService;

            _itemRepository = itemRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemAttachmentRepository", _itemAttachmentRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemRepository", _itemRepository));
        }
    }
}