using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.PriceLists;

namespace DMSpro.OMS.MdmService.Controllers.PriceLists
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("PriceList")]
    [Route("api/mdm-service/price-lists")]
    public partial class PriceListController : AbpController, IPriceListsAppService
    {
        private readonly IPriceListsAppService _priceListsAppService;

        public PriceListController(IPriceListsAppService priceListsAppService)
        {
            _priceListsAppService = priceListsAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<PriceListDto> GetAsync(Guid id)
        {
            return _priceListsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<PriceListDto> CreateAsync(PriceListCreateDto input)
        {
            return _priceListsAppService.CreateAsync(input);

        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<PriceListDto> UpdateAsync(Guid id, PriceListUpdateDto input)
        {
            return _priceListsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _priceListsAppService.DeleteAsync(id);
        }
    }
}