using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CustomerAttributes;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;




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
        public virtual Task<PagedResultDto<CustomerAttributeDto>> GetListAsync(GetCustomerAttributesInput input)
        {
            return _customerAttributesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerAttributeDto> GetAsync(Guid id)
        {
            return _customerAttributesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<CustomerAttributeDto> CreateAsync(CustomerAttributeCreateDto input)
        {
            return _customerAttributesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CustomerAttributeDto> UpdateAsync(Guid id, CustomerAttributeUpdateDto input)
        {
            return _customerAttributesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _customerAttributesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerAttributeExcelDownloadDto input)
        {
            return _customerAttributesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _customerAttributesAppService.GetDownloadTokenAsync();
        }
    }
}