using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CustomerAttachments;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.CustomerAttachments
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("CustomerAttachment")]
    [Route("api/mdm-service/customer-attachments")]
    public partial class CustomerAttachmentController : AbpController, ICustomerAttachmentsAppService
    {
        private readonly ICustomerAttachmentsAppService _customerAttachmentsAppService;

        public CustomerAttachmentController(ICustomerAttachmentsAppService customerAttachmentsAppService)
        {
            _customerAttachmentsAppService = customerAttachmentsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<CustomerAttachmentWithNavigationPropertiesDto>> GetListAsync(GetCustomerAttachmentsInput input)
        {
            return _customerAttachmentsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<CustomerAttachmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _customerAttachmentsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerAttachmentDto> GetAsync(Guid id)
        {
            return _customerAttachmentsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("customer-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input)
        {
            return _customerAttachmentsAppService.GetCustomerLookupAsync(input);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerAttachmentExcelDownloadDto input)
        {
            return _customerAttachmentsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _customerAttachmentsAppService.GetDownloadTokenAsync();
        }
    }
}