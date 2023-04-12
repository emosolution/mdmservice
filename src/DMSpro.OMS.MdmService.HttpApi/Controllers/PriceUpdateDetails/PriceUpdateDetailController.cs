using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.PriceUpdateDetails;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.PriceUpdateDetails
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("PriceUpdateDetail")]
    [Route("api/mdm-service/price-update-details")]
    public partial class PriceUpdateDetailController : AbpController, IPriceUpdateDetailsAppService
    {
        private readonly IPriceUpdateDetailsAppService _priceUpdateDetailsAppService;

        public PriceUpdateDetailController(IPriceUpdateDetailsAppService priceUpdateDetailsAppService)
        {
            _priceUpdateDetailsAppService = priceUpdateDetailsAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<PriceUpdateDetailDto> GetAsync(Guid id)
        {
            return _priceUpdateDetailsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<PriceUpdateDetailDto> CreateAsync(PriceUpdateDetailCreateDto input)
        {
            return _priceUpdateDetailsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<PriceUpdateDetailDto> UpdateAsync(Guid id, PriceUpdateDetailUpdateDto input)
        {
            return _priceUpdateDetailsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _priceUpdateDetailsAppService.DeleteAsync(id);
        }
    }
}