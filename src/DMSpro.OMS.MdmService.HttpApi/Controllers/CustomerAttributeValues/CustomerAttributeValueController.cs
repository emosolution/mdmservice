using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("CustomerAttributeValue")]
    [Route("api/mdm-service/customer-attribute-values")]
    public class CustomerAttributeValueController : AbpController, ICustomerAttributeValuesAppService
    {
        private readonly ICustomerAttributeValuesAppService _customerAttributeValuesAppService;

        public CustomerAttributeValueController(ICustomerAttributeValuesAppService customerAttributeValuesAppService)
        {
            _customerAttributeValuesAppService = customerAttributeValuesAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<CustomerAttributeValueWithNavigationPropertiesDto>> GetListAsync(GetCustomerAttributeValuesInput input)
        {
            return _customerAttributeValuesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<CustomerAttributeValueWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _customerAttributeValuesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerAttributeValueDto> GetAsync(Guid id)
        {
            return _customerAttributeValuesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("customer-attribute-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCustomerAttributeLookupAsync(LookupRequestDto input)
        {
            return _customerAttributeValuesAppService.GetCustomerAttributeLookupAsync(input);
        }

        [HttpGet]
        [Route("customer-attribute-value-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCustomerAttributeValueLookupAsync(LookupRequestDto input)
        {
            return _customerAttributeValuesAppService.GetCustomerAttributeValueLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<CustomerAttributeValueDto> CreateAsync(CustomerAttributeValueCreateDto input)
        {
            return _customerAttributeValuesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CustomerAttributeValueDto> UpdateAsync(Guid id, CustomerAttributeValueUpdateDto input)
        {
            return _customerAttributeValuesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _customerAttributeValuesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerAttributeValueExcelDownloadDto input)
        {
            return _customerAttributeValuesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _customerAttributeValuesAppService.GetDownloadTokenAsync();
        }
    }
}