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
        public virtual async Task<PriceListDto> GetAsync(Guid id)
        {
            try { 
            return await _priceListsAppService.GetAsync(id);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }

        [HttpPost]
        public virtual async Task<PriceListDto> CreateAsync(PriceListCreateDto input)
        {
            try
            {
                return await _priceListsAppService.CreateAsync(input);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public virtual async Task<PriceListDto> UpdateAsync(Guid id, PriceListUpdateDto input)
        {
            try
            {
                return await _priceListsAppService.UpdateAsync(id, input);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual async Task DeleteAsync(Guid id)
        {
            try
            {
                await _priceListsAppService.DeleteAsync(id);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }
    }
}