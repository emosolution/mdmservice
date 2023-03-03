using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.ItemAttachments;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.ItemImages;
using System.ComponentModel.DataAnnotations;

namespace DMSpro.OMS.MdmService.Controllers.ItemAttachments
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("ItemAttachment")]
    [Route("api/mdm-service/item-attachments")]
    public partial class ItemAttachmentController : AbpController, IItemAttachmentsAppService
    {
        private readonly IItemAttachmentsAppService _itemAttachmentsAppService;

        public ItemAttachmentController(IItemAttachmentsAppService itemAttachmentsAppService)
        {
            _itemAttachmentsAppService = itemAttachmentsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<ItemAttachmentWithNavigationPropertiesDto>> GetListAsync(GetItemAttachmentsInput input)
        {
            return _itemAttachmentsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<ItemAttachmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _itemAttachmentsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ItemAttachmentDto> GetAsync(Guid id)
        {
            return _itemAttachmentsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("item-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetItemLookupAsync(LookupRequestDto input)
        {
            return _itemAttachmentsAppService.GetItemLookupAsync(input);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemAttachmentExcelDownloadDto input)
        {
            return _itemAttachmentsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _itemAttachmentsAppService.GetDownloadTokenAsync();
        }
    }
}