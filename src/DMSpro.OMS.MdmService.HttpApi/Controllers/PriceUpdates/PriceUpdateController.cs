using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.PriceUpdates;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

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
        public Task<PagedResultDto<PriceUpdateWithNavigationPropertiesDto>> GetListAsync(GetPriceUpdatesInput input)
        {
            return _priceUpdatesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<PriceUpdateWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _priceUpdatesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<PriceUpdateDto> GetAsync(Guid id)
        {
            return _priceUpdatesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("price-list-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetPriceListLookupAsync(LookupRequestDto input)
        {
            return _priceUpdatesAppService.GetPriceListLookupAsync(input);
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

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _priceUpdatesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(PriceUpdateExcelDownloadDto input)
        {
            return _priceUpdatesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _priceUpdatesAppService.GetDownloadTokenAsync();
        }
    }
}