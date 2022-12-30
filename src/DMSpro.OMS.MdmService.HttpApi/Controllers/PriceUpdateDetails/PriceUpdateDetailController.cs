using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.PriceUpdateDetails;
using Volo.Abp.Content;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.PriceUpdateDetails
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("PriceUpdateDetail")]
    [Route("api/mdm-service/price-update-details")]
    public class PriceUpdateDetailController : AbpController, IPriceUpdateDetailsAppService
    {
        private readonly IPriceUpdateDetailsAppService _priceUpdateDetailsAppService;

        public PriceUpdateDetailController(IPriceUpdateDetailsAppService priceUpdateDetailsAppService)
        {
            _priceUpdateDetailsAppService = priceUpdateDetailsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<PriceUpdateDetailWithNavigationPropertiesDto>> GetListAsync(GetPriceUpdateDetailsInput input)
        {
            return _priceUpdateDetailsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<PriceUpdateDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _priceUpdateDetailsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _priceUpdateDetailsAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<PriceUpdateDetailDto> GetAsync(Guid id)
        {
            return _priceUpdateDetailsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("price-update-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetPriceUpdateLookupAsync(LookupRequestDto input)
        {
            return _priceUpdateDetailsAppService.GetPriceUpdateLookupAsync(input);
        }

        [HttpGet]
        [Route("price-list-detail-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetPriceListDetailLookupAsync(LookupRequestDto input)
        {
            return _priceUpdateDetailsAppService.GetPriceListDetailLookupAsync(input);
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

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(PriceUpdateDetailExcelDownloadDto input)
        {
            return _priceUpdateDetailsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _priceUpdateDetailsAppService.GetDownloadTokenAsync();
        }
    }
}