using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CustomerInZones;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Controllers.CustomerInZones
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("CustomerInZone")]
    [Route("api/mdm-service/customer-in-zones")]
    public partial class CustomerInZoneController : AbpController, ICustomerInZonesAppService
    {
        private readonly ICustomerInZonesAppService _customerInZonesAppService;

        public CustomerInZoneController(ICustomerInZonesAppService customerInZonesAppService)
        {
            _customerInZonesAppService = customerInZonesAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<CustomerInZoneWithNavigationPropertiesDto>> GetListAsync(GetCustomerInZonesInput input)
        {
            return _customerInZonesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<CustomerInZoneWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _customerInZonesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerInZoneDto> GetAsync(Guid id)
        {
            return _customerInZonesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("sales-org-hierarchy-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input)
        {
            return _customerInZonesAppService.GetSalesOrgHierarchyLookupAsync(input);
        }

        [HttpGet]
        [Route("customer-profile-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input)
        {
            return _customerInZonesAppService.GetCustomerLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<CustomerInZoneDto> CreateAsync(CustomerInZoneCreateDto input)
        {
            return _customerInZonesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CustomerInZoneDto> UpdateAsync(Guid id, CustomerInZoneUpdateDto input)
        {
            return _customerInZonesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _customerInZonesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerInZoneExcelDownloadDto input)
        {
            return _customerInZonesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _customerInZonesAppService.GetDownloadTokenAsync();
        }
    }
}