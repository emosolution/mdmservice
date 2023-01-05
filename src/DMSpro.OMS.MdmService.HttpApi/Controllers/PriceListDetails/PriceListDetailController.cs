using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.PriceListDetails;
using Volo.Abp.Content;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.PriceListDetails
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("PriceListDetail")]
    [Route("api/mdm-service/price-list-details")]
    public class PriceListDetailController : AbpController, IPriceListDetailsAppService
    {
        private readonly IPriceListDetailsAppService _priceListDetailsAppService;

        public PriceListDetailController(IPriceListDetailsAppService priceListDetailsAppService)
        {
            _priceListDetailsAppService = priceListDetailsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<PriceListDetailWithNavigationPropertiesDto>> GetListAsync(GetPriceListDetailsInput input)
        {
            return _priceListDetailsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<PriceListDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _priceListDetailsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _priceListDetailsAppService.GetListDevextremesAsync(inputDev);
        }
        
        [HttpGet]
        [Route("{id}")]
        public virtual Task<PriceListDetailDto> GetAsync(Guid id)
        {
            return _priceListDetailsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("price-list-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetPriceListLookupAsync(LookupRequestDto input)
        {
            return _priceListDetailsAppService.GetPriceListLookupAsync(input);
        }

        [HttpGet]
        [Route("u-oM-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetUOMLookupAsync(LookupRequestDto input)
        {
            return _priceListDetailsAppService.GetUOMLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<PriceListDetailDto> CreateAsync(PriceListDetailCreateDto input)
        {
            return _priceListDetailsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<PriceListDetailDto> UpdateAsync(Guid id, PriceListDetailUpdateDto input)
        {
            return _priceListDetailsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _priceListDetailsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(PriceListDetailExcelDownloadDto input)
        {
            return _priceListDetailsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _priceListDetailsAppService.GetDownloadTokenAsync();
        }
    }
}