using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.CustomerGroups;

namespace DMSpro.OMS.MdmService.Controllers.CustomerGroups
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("CustomerGroup")]
    [Route("api/mdm-service/customer-groups")]
    public partial class CustomerGroupController : AbpController, ICustomerGroupsAppService
    {
        private readonly ICustomerGroupsAppService _customerGroupsAppService;

        public CustomerGroupController(ICustomerGroupsAppService customerGroupsAppService)
        {
            _customerGroupsAppService = customerGroupsAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerGroupDto> GetAsync(Guid id)
        {
            return _customerGroupsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<CustomerGroupDto> CreateAsync(CustomerGroupCreateDto input)
        {
            return _customerGroupsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CustomerGroupDto> UpdateAsync(Guid id, CustomerGroupUpdateDto input)
        {
            return _customerGroupsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("release/{id}")]
        public virtual Task ReleaseAsync(Guid id)
        {
            return _customerGroupsAppService.ReleaseAsync(id);
        }
    }
}