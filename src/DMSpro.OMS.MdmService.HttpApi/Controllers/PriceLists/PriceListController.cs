using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.PriceLists;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.PriceLists
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("PriceList")]
    [Route("api/mdm-service/price-lists")]
    public class PriceListController : AbpController, IPriceListsAppService
    {
        private readonly IPriceListsAppService _priceListsAppService;

        public PriceListController(IPriceListsAppService priceListsAppService)
        {
            _priceListsAppService = priceListsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<PriceListWithNavigationPropertiesDto>> GetListAsync(GetPriceListsInput input)
        {
            return _priceListsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<PriceListWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _priceListsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _priceListsAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<PriceListDto> GetAsync(Guid id)
        {
            return _priceListsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("price-list-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetPriceListLookupAsync(LookupRequestDto input)
        {
            return _priceListsAppService.GetPriceListLookupAsync(input);
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

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(PriceListExcelDownloadDto input)
        {
            return _priceListsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _priceListsAppService.GetDownloadTokenAsync();
        }
    }
}