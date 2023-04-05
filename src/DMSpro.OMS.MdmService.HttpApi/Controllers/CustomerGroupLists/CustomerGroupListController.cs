using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CustomerGroupLists;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

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
        public Task<PagedResultDto<CustomerGroupListWithNavigationPropertiesDto>> GetListAsync(GetCustomerGroupListsInput input)
        {
            return _customerGroupListsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<CustomerGroupListWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _customerGroupListsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerGroupListDto> GetAsync(Guid id)
        {
            return _customerGroupListsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("customer-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input)
        {
            return _customerGroupListsAppService.GetCustomerLookupAsync(input);
        }

        [HttpGet]
        [Route("customer-group-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCustomerGroupLookupAsync(LookupRequestDto input)
        {
            return _customerGroupListsAppService.GetCustomerGroupLookupAsync(input);
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

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerGroupListExcelDownloadDto input)
        {
            return _customerGroupListsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _customerGroupListsAppService.GetDownloadTokenAsync();
        }
    }
}