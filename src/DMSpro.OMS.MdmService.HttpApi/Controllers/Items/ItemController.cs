using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Items;
using Volo.Abp.Content;
using Volo.Abp.Json;
using Volo.Abp.Uow;

namespace DMSpro.OMS.MdmService.Controllers.Items
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("Item")]
    [Route("api/mdm-service/items")]
    public partial class ItemController : AbpController, IItemsAppService
    {
        private readonly IItemsAppService _itemsAppService;
        private readonly IJsonSerializer _jsonSerializer;
        public ItemController(IItemsAppService itemsAppService, IJsonSerializer jsonSerializer)
        {
            _itemsAppService = itemsAppService;
            _jsonSerializer = jsonSerializer;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ItemDto> GetAsync(Guid id)
        {
            return _itemsAppService.GetAsync(id);
        }

        
        [HttpPost]
        public virtual Task<ItemDto> CreateAsync(ItemCreateDto input)
        {
            return _itemsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        [UnitOfWork(IsDisabled = true)]

        public virtual Task<ItemDto> UpdateAsync(Guid id, ItemUpdateDto input)
        {
            return _itemsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        [UnitOfWork(IsDisabled = true)]

        public virtual Task DeleteAsync(Guid id)
        {
            return _itemsAppService.DeleteAsync(id);
        }
    }
}