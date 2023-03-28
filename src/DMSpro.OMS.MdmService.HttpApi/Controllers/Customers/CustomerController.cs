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
        [Route("{id}")]
        public virtual Task<CustomerDto> GetAsync(Guid id)
        {
            return _customersAppService.GetAsync(id);
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
    }
}