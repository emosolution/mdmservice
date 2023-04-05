using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CustomerGroupGeos;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("CustomerGroupGeo")]
    [Route("api/mdm-service/customer-group-geos")]
    public class CustomerGroupGeoController : AbpController, ICustomerGroupGeosAppService
    {
        private readonly ICustomerGroupGeosAppService _customerGroupGeosAppService;

        public CustomerGroupGeoController(ICustomerGroupGeosAppService customerGroupGeosAppService)
        {
            _customerGroupGeosAppService = customerGroupGeosAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<CustomerGroupGeoWithNavigationPropertiesDto>> GetListAsync(GetCustomerGroupGeosInput input)
        {
            return _customerGroupGeosAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<CustomerGroupGeoWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _customerGroupGeosAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerGroupGeoDto> GetAsync(Guid id)
        {
            return _customerGroupGeosAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("customer-group-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCustomerGroupLookupAsync(LookupRequestDto input)
        {
            return _customerGroupGeosAppService.GetCustomerGroupLookupAsync(input);
        }

        [HttpGet]
        [Route("geo-master-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetGeoMasterLookupAsync(LookupRequestDto input)
        {
            return _customerGroupGeosAppService.GetGeoMasterLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<CustomerGroupGeoDto> CreateAsync(CustomerGroupGeoCreateDto input)
        {
            return _customerGroupGeosAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CustomerGroupGeoDto> UpdateAsync(Guid id, CustomerGroupGeoUpdateDto input)
        {
            return _customerGroupGeosAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _customerGroupGeosAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerGroupGeoExcelDownloadDto input)
        {
            return _customerGroupGeosAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _customerGroupGeosAppService.GetDownloadTokenAsync();
        }
    }
}