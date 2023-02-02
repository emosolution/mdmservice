using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.ItemAttributes;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Controllers.ItemAttributes
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("ItemAttribute")]
    [Route("api/mdm-service/item-attributes")]
    public partial class ItemAttributeController : AbpController, IItemAttributesAppService
    {
        private readonly IItemAttributesAppService _itemAttributesAppService;

        public ItemAttributeController(IItemAttributesAppService itemAttributesAppService)
        {
            _itemAttributesAppService = itemAttributesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<ItemAttributeDto>> GetListAsync(GetItemAttributesInput input)
        {
            return _itemAttributesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ItemAttributeDto> GetAsync(Guid id)
        {
            return _itemAttributesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<ItemAttributeDto> CreateAsync(ItemAttributeCreateDto input)
        {
            return _itemAttributesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ItemAttributeDto> UpdateAsync(Guid id, ItemAttributeUpdateDto input)
        {
            return _itemAttributesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _itemAttributesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemAttributeExcelDownloadDto input)
        {
            return _itemAttributesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _itemAttributesAppService.GetDownloadTokenAsync();
        }
    }
}