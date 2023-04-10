using System;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.CustomerGroupAttributes;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace DMSpro.OMS.MdmService.Controllers.CustomerGroupAttributes
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("CustomerGroupAttribute")]
    [Route("api/mdm-service/customer-group-attributes")]
    public partial class CustomerGroupAttributeController : AbpController, ICustomerGroupAttributesAppService
    {
        private readonly ICustomerGroupAttributesAppService _customerGroupAttributesAppService;

        public CustomerGroupAttributeController(ICustomerGroupAttributesAppService customerGroupAttributesAppService)
        {
            _customerGroupAttributesAppService = customerGroupAttributesAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerGroupAttributeDto> GetAsync(Guid id)
        {
            return _customerGroupAttributesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<CustomerGroupAttributeDto> CreateAsync(CustomerGroupAttributeCreateDto input)
        {
            return _customerGroupAttributesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CustomerGroupAttributeDto> UpdateAsync(Guid id, CustomerGroupAttributeUpdateDto input)
        {
            return _customerGroupAttributesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _customerGroupAttributesAppService.DeleteAsync(id);
        }
    }
}