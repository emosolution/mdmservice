using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Customers;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.Customers
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("Customer")]
    [Route("api/mdm-service/customers")]
    public partial class CustomerController : AbpController, ICustomersAppService
    {
        private readonly ICustomersAppService _customersAppService;

        public CustomerController(ICustomersAppService customersAppService)
        {
            _customersAppService = customersAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<CustomerWithNavigationPropertiesDto>> GetListAsync(GetCustomersInput input)
        {
            return _customersAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<CustomerWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _customersAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerDto> GetAsync(Guid id)
        {
            return _customersAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("system-data-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetSystemDataLookupAsync(LookupRequestDto input)
        {
            return _customersAppService.GetSystemDataLookupAsync(input);
        }

        [HttpGet]
        [Route("company-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            return _customersAppService.GetCompanyLookupAsync(input);
        }

        [HttpGet]
        [Route("price-list-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetPriceListLookupAsync(LookupRequestDto input)
        {
            return _customersAppService.GetPriceListLookupAsync(input);
        }

        [HttpGet]
        [Route("geo-master-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetGeoMasterLookupAsync(LookupRequestDto input)
        {
            return _customersAppService.GetGeoMasterLookupAsync(input);
        }

        [HttpGet]
        [Route("cus-attribute-value-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCusAttributeValueLookupAsync(LookupRequestDto input)
        {
            return _customersAppService.GetCusAttributeValueLookupAsync(input);
        }

        [HttpGet]
        [Route("customer-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input)
        {
            return _customersAppService.GetCustomerLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<CustomerDto> CreateAsync(CustomerCreateDto input)
        {
            return _customersAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CustomerDto> UpdateAsync(Guid id, CustomerUpdateDto input)
        {
            return _customersAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _customersAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerExcelDownloadDto input)
        {
            return _customersAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _customersAppService.GetDownloadTokenAsync();
        }
    }
}