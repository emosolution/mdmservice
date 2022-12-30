using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.ItemAttachments;
using Volo.Abp.Content;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.ItemAttachments
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("ItemAttachment")]
    [Route("api/mdm-service/item-attachments")]
    public class ItemAttachmentController : AbpController, IItemAttachmentsAppService
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
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _itemAttachmentsAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ItemAttachmentDto> GetAsync(Guid id)
        {
            return _itemAttachmentsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("item-master-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetItemMasterLookupAsync(LookupRequestDto input)
        {
            return _itemAttachmentsAppService.GetItemMasterLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<ItemAttachmentDto> CreateAsync(ItemAttachmentCreateDto input)
        {
            return _itemAttachmentsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ItemAttachmentDto> UpdateAsync(Guid id, ItemAttachmentUpdateDto input)
        {
            return _itemAttachmentsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _itemAttachmentsAppService.DeleteAsync(id);
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