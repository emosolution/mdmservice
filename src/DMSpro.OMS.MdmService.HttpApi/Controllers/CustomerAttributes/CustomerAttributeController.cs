using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.CustomerAttributes;
using DevExtreme.AspNet.Data.ResponseModel;

namespace DMSpro.OMS.MdmService.Controllers.CustomerAttributes
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("CustomerAttribute")]
    [Route("api/mdm-service/customer-attributes")]
    public partial class CustomerAttributeController : AbpController, ICustomerAttributesAppService
    {
        private readonly ICustomerAttributesAppService _customerAttributesAppService;

        public CustomerAttributeController(ICustomerAttributesAppService customerAttributesAppService)
        {
            _customerAttributesAppService = customerAttributesAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerAttributeDto> GetAsync(Guid id)
        {
            return _customerAttributesAppService.GetAsync(id);
        }


        [HttpPut]
        [Route("{id}")]
        public virtual Task<LoadResult> UpdateAsync(Guid id, CustomerAttributeUpdateDto input)
        {
            return _customerAttributesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task<LoadResult> DeleteAsync()
        {
            return _customerAttributesAppService.DeleteAsync();
        }

        [HttpPost]
        public Task<LoadResult> CreateAsync(CustomerAttributeCreateDto input)
        {
            return _customerAttributesAppService.CreateAsync(input);
        }
    }
}