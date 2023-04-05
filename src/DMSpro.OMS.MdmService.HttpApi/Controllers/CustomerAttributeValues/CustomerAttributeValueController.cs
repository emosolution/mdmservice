using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using Volo.Abp.Content;

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
        [Route("{id}")]
        public virtual Task<CustomerAttributeValueDto> GetAsync(Guid id)
        {
            return _customerAttributeValuesAppService.GetAsync(id);
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
    }
}