using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.ProductAttributes;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.ProductAttributes
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("ProductAttribute")]
    [Route("api/mdm-service/product-attributes")]
    public class ProductAttributeController : AbpController, IProductAttributesAppService
    {
        private readonly IProductAttributesAppService _productAttributesAppService;

        public ProductAttributeController(IProductAttributesAppService productAttributesAppService)
        {
            _productAttributesAppService = productAttributesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<ProductAttributeDto>> GetListAsync(GetProductAttributesInput input)
        {
            return _productAttributesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _productAttributesAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ProductAttributeDto> GetAsync(Guid id)
        {
            return _productAttributesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<ProductAttributeDto> CreateAsync(ProductAttributeCreateDto input)
        {
            return _productAttributesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ProductAttributeDto> UpdateAsync(Guid id, ProductAttributeUpdateDto input)
        {
            return _productAttributesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _productAttributesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(ProductAttributeExcelDownloadDto input)
        {
            return _productAttributesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _productAttributesAppService.GetDownloadTokenAsync();
        }
    }
}