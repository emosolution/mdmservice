using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CusAttributeValues;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.CusAttributeValues
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("CusAttributeValue")]
    [Route("api/mdm-service/cus-attribute-values")]
    public class CusAttributeValueController : AbpController, ICusAttributeValuesAppService
    {
        private readonly ICusAttributeValuesAppService _cusAttributeValuesAppService;

        public CusAttributeValueController(ICusAttributeValuesAppService cusAttributeValuesAppService)
        {
            _cusAttributeValuesAppService = cusAttributeValuesAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<CusAttributeValueWithNavigationPropertiesDto>> GetListAsync(GetCusAttributeValuesInput input)
        {
            return _cusAttributeValuesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<CusAttributeValueWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _cusAttributeValuesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _cusAttributeValuesAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CusAttributeValueDto> GetAsync(Guid id)
        {
            return _cusAttributeValuesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("customer-attribute-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCustomerAttributeLookupAsync(LookupRequestDto input)
        {
            return _cusAttributeValuesAppService.GetCustomerAttributeLookupAsync(input);
        }

        [HttpGet]
        [Route("cus-attribute-value-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetCusAttributeValueLookupAsync(LookupRequestDto input)
        {
            return _cusAttributeValuesAppService.GetCusAttributeValueLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<CusAttributeValueDto> CreateAsync(CusAttributeValueCreateDto input)
        {
            return _cusAttributeValuesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CusAttributeValueDto> UpdateAsync(Guid id, CusAttributeValueUpdateDto input)
        {
            return _cusAttributeValuesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _cusAttributeValuesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CusAttributeValueExcelDownloadDto input)
        {
            return _cusAttributeValuesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _cusAttributeValuesAppService.GetDownloadTokenAsync();
        }
    }
}