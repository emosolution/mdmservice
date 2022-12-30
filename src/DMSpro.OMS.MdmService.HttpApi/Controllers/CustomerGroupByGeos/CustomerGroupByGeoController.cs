using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CustomerGroupByGeos;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.CustomerGroupByGeos
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("CustomerGroupByGeo")]
    [Route("api/mdm-service/customer-group-by-geos")]
    public class CustomerGroupByGeoController : AbpController, ICustomerGroupByGeosAppService
    {
        private readonly ICustomerGroupByGeosAppService _customerGroupByGeosAppService;

        public CustomerGroupByGeoController(ICustomerGroupByGeosAppService customerGroupByGeosAppService)
        {
            _customerGroupByGeosAppService = customerGroupByGeosAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<CustomerGroupByGeoWithNavigationPropertiesDto>> GetListAsync(GetCustomerGroupByGeosInput input)
        {
            return _customerGroupByGeosAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _customerGroupByGeosAppService.GetListDevextremesAsync(inputDev);
        }
        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<CustomerGroupByGeoWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _customerGroupByGeosAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerGroupByGeoDto> GetAsync(Guid id)
        {
            return _customerGroupByGeosAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("customer-group-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCustomerGroupLookupAsync(LookupRequestDto input)
        {
            return _customerGroupByGeosAppService.GetCustomerGroupLookupAsync(input);
        }

        [HttpGet]
        [Route("geo-master-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetGeoMasterLookupAsync(LookupRequestDto input)
        {
            return _customerGroupByGeosAppService.GetGeoMasterLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<CustomerGroupByGeoDto> CreateAsync(CustomerGroupByGeoCreateDto input)
        {
            return _customerGroupByGeosAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CustomerGroupByGeoDto> UpdateAsync(Guid id, CustomerGroupByGeoUpdateDto input)
        {
            return _customerGroupByGeosAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _customerGroupByGeosAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerGroupByGeoExcelDownloadDto input)
        {
            return _customerGroupByGeosAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _customerGroupByGeosAppService.GetDownloadTokenAsync();
        }
    }
}