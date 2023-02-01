using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CustomerGroups;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;





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
        public virtual Task<PagedResultDto<CustomerGroupDto>> GetListAsync(GetCustomerGroupsInput input)
        {
            return _customerGroupsAppService.GetListAsync(input);
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
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _customerGroupsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerGroupExcelDownloadDto input)
        {
            return _customerGroupsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _customerGroupsAppService.GetDownloadTokenAsync();
        }
    }
}