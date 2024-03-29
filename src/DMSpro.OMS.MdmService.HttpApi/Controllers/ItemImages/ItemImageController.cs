using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.ItemImages;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.ItemImages
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("ItemImage")]
    [Route("api/mdm-service/item-images")]
    public partial class ItemImageController : AbpController, IItemImagesAppService
    {
        private readonly IItemImagesAppService _itemImagesAppService;

        public ItemImageController(IItemImagesAppService itemImagesAppService)
        {
            _itemImagesAppService = itemImagesAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<ItemImageWithNavigationPropertiesDto>> GetListAsync(GetItemImagesInput input)
        {
            return _itemImagesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<ItemImageWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _itemImagesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ItemImageDto> GetAsync(Guid id)
        {
            return _itemImagesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("item-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetItemLookupAsync(LookupRequestDto input)
        {
            return _itemImagesAppService.GetItemLookupAsync(input);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemImageExcelDownloadDto input)
        {
            return _itemImagesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _itemImagesAppService.GetDownloadTokenAsync();
        }
    }
}