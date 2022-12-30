using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CustomerGroupByLists;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.CustomerGroupByLists
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("CustomerGroupByList")]
    [Route("api/mdm-service/customer-group-by-lists")]
    public class CustomerGroupByListController : AbpController, ICustomerGroupByListsAppService
    {
        private readonly ICustomerGroupByListsAppService _customerGroupByListsAppService;

        public CustomerGroupByListController(ICustomerGroupByListsAppService customerGroupByListsAppService)
        {
            _customerGroupByListsAppService = customerGroupByListsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<CustomerGroupByListWithNavigationPropertiesDto>> GetListAsync(GetCustomerGroupByListsInput input)
        {
            return _customerGroupByListsAppService.GetListAsync(input);
        }
        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _customerGroupByListsAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<CustomerGroupByListWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _customerGroupByListsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerGroupByListDto> GetAsync(Guid id)
        {
            return _customerGroupByListsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("customer-group-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCustomerGroupLookupAsync(LookupRequestDto input)
        {
            return _customerGroupByListsAppService.GetCustomerGroupLookupAsync(input);
        }

        [HttpGet]
        [Route("customer-profile-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input)
        {
            return _customerGroupByListsAppService.GetCustomerLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<CustomerGroupByListDto> CreateAsync(CustomerGroupByListCreateDto input)
        {
            return _customerGroupByListsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CustomerGroupByListDto> UpdateAsync(Guid id, CustomerGroupByListUpdateDto input)
        {
            return _customerGroupByListsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _customerGroupByListsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerGroupByListExcelDownloadDto input)
        {
            return _customerGroupByListsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _customerGroupByListsAppService.GetDownloadTokenAsync();
        }
    }
}