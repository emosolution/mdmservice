using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.ProdAttributeValues;
using Volo.Abp.Content;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.ProdAttributeValues
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("ProdAttributeValue")]
    [Route("api/mdm-service/prod-attribute-values")]
    public class ProdAttributeValueController : AbpController, IProdAttributeValuesAppService
    {
        private readonly IProdAttributeValuesAppService _prodAttributeValuesAppService;

        public ProdAttributeValueController(IProdAttributeValuesAppService prodAttributeValuesAppService)
        {
            _prodAttributeValuesAppService = prodAttributeValuesAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<ProdAttributeValueWithNavigationPropertiesDto>> GetListAsync(GetProdAttributeValuesInput input)
        {
            return _prodAttributeValuesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<ProdAttributeValueWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _prodAttributeValuesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _prodAttributeValuesAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ProdAttributeValueDto> GetAsync(Guid id)
        {
            return _prodAttributeValuesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("product-attribute-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetProductAttributeLookupAsync(LookupRequestDto input)
        {
            return _prodAttributeValuesAppService.GetProductAttributeLookupAsync(input);
        }

        [HttpGet]
        [Route("prod-attribute-value-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetProdAttributeValueLookupAsync(LookupRequestDto input)
        {
            return _prodAttributeValuesAppService.GetProdAttributeValueLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<ProdAttributeValueDto> CreateAsync(ProdAttributeValueCreateDto input)
        {
            return _prodAttributeValuesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ProdAttributeValueDto> UpdateAsync(Guid id, ProdAttributeValueUpdateDto input)
        {
            return _prodAttributeValuesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _prodAttributeValuesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(ProdAttributeValueExcelDownloadDto input)
        {
            return _prodAttributeValuesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _prodAttributeValuesAppService.GetDownloadTokenAsync();
        }
    }
}