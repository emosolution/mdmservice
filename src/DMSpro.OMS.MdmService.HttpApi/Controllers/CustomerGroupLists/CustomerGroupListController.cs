using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("CustomerGroupList")]
    [Route("api/mdm-service/customer-group-lists")]
    public class CustomerGroupListController : AbpController, ICustomerGroupListsAppService
    {
        private readonly ICustomerGroupListsAppService _customerGroupListsAppService;

        public CustomerGroupListController(ICustomerGroupListsAppService customerGroupListsAppService)
        {
            _customerGroupListsAppService = customerGroupListsAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerGroupListDto> GetAsync(Guid id)
        {
            return _customerGroupListsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<CustomerGroupListDto> CreateAsync(CustomerGroupListCreateDto input)
        {
            return _customerGroupListsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CustomerGroupListDto> UpdateAsync(Guid id, CustomerGroupListUpdateDto input)
        {
            return _customerGroupListsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _customerGroupListsAppService.DeleteAsync(id);
        }
    }
}