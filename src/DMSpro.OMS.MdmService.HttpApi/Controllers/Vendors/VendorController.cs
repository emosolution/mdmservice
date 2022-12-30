using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Vendors;
using Volo.Abp.Content;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.Vendors
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("Vendor")]
    [Route("api/mdm-service/vendors")]
    public class VendorController : AbpController, IVendorsAppService
    {
        private readonly IVendorsAppService _vendorsAppService;

        public VendorController(IVendorsAppService vendorsAppService)
        {
            _vendorsAppService = vendorsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<VendorWithNavigationPropertiesDto>> GetListAsync(GetVendorsInput input)
        {
            return _vendorsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<VendorWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _vendorsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _vendorsAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<VendorDto> GetAsync(Guid id)
        {
            return _vendorsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("company-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            return _vendorsAppService.GetCompanyLookupAsync(input);
        }

        [HttpGet]
        [Route("price-list-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetPriceListLookupAsync(LookupRequestDto input)
        {
            return _vendorsAppService.GetPriceListLookupAsync(input);
        }

        [HttpGet]
        [Route("geo-master-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetGeoMasterLookupAsync(LookupRequestDto input)
        {
            return _vendorsAppService.GetGeoMasterLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<VendorDto> CreateAsync(VendorCreateDto input)
        {
            return _vendorsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<VendorDto> UpdateAsync(Guid id, VendorUpdateDto input)
        {
            return _vendorsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _vendorsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(VendorExcelDownloadDto input)
        {
            return _vendorsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _vendorsAppService.GetDownloadTokenAsync();
        }
    }
}