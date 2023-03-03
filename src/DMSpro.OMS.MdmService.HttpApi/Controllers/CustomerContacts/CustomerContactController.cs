using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CustomerContacts;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Controllers.CustomerContacts
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("CustomerContact")]
    [Route("api/mdm-service/customer-contacts")]
    public partial class CustomerContactController : AbpController, ICustomerContactsAppService
    {
        private readonly ICustomerContactsAppService _customerContactsAppService;

        public CustomerContactController(ICustomerContactsAppService customerContactsAppService)
        {
            _customerContactsAppService = customerContactsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<CustomerContactWithNavigationPropertiesDto>> GetListAsync(GetCustomerContactsInput input)
        {
            return _customerContactsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<CustomerContactWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _customerContactsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerContactDto> GetAsync(Guid id)
        {
            return _customerContactsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("customer-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input)
        {
            return _customerContactsAppService.GetCustomerLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<CustomerContactDto> CreateAsync(CustomerContactCreateDto input)
        {
            return _customerContactsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CustomerContactDto> UpdateAsync(Guid id, CustomerContactUpdateDto input)
        {
            return _customerContactsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _customerContactsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerContactExcelDownloadDto input)
        {
            return _customerContactsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _customerContactsAppService.GetDownloadTokenAsync();
        }
    }
}