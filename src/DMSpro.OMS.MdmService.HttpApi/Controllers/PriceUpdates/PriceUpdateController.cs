using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.PriceUpdates;

namespace DMSpro.OMS.MdmService.Controllers.PriceUpdates
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("PriceUpdate")]
    [Route("api/mdm-service/price-updates")]
    public partial class PriceUpdateController : AbpController, IPriceUpdatesAppService
    {
        private readonly IPriceUpdatesAppService _priceUpdatesAppService;

        public PriceUpdateController(IPriceUpdatesAppService priceUpdatesAppService)
        {
            _priceUpdatesAppService = priceUpdatesAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<PriceUpdateDto> GetAsync(Guid id)
        {
            return _priceUpdatesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<PriceUpdateDto> CreateAsync(PriceUpdateCreateDto input)
        {
            return _priceUpdatesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<PriceUpdateDto> UpdateAsync(Guid id, PriceUpdateUpdateDto input)
        {
            return _priceUpdatesAppService.UpdateAsync(id, input);
        }

        [HttpPut]
        [Route("cancel/{id}")]
        public virtual Task<PriceUpdateDto> CancelAsync(Guid id)
        {
            return _priceUpdatesAppService.CancelAsync(id);
        }

        [HttpPut]
        [Route("release/{id}")]
        public virtual Task<PriceUpdateDto> ReleaseAsync(Guid id)
        {
            return _priceUpdatesAppService.ReleaseAsync(id);
        }
    }
}