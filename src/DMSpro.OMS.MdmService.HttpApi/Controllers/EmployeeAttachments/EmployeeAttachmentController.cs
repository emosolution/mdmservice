using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.EmployeeAttachments;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.EmployeeAttachments
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("EmployeeAttachment")]
    [Route("api/mdm-service/employee-attachments")]
    public partial class EmployeeAttachmentController : AbpController, IEmployeeAttachmentsAppService
    {
        private readonly IEmployeeAttachmentsAppService _employeeAttachmentsAppService;

        public EmployeeAttachmentController(IEmployeeAttachmentsAppService employeeAttachmentsAppService)
        {
            _employeeAttachmentsAppService = employeeAttachmentsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<EmployeeAttachmentWithNavigationPropertiesDto>> GetListAsync(GetEmployeeAttachmentsInput input)
        {
            return _employeeAttachmentsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<EmployeeAttachmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _employeeAttachmentsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<EmployeeAttachmentDto> GetAsync(Guid id)
        {
            return _employeeAttachmentsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("employee-profile-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeProfileLookupAsync(LookupRequestDto input)
        {
            return _employeeAttachmentsAppService.GetEmployeeProfileLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<EmployeeAttachmentDto> CreateAsync(EmployeeAttachmentCreateDto input)
        {
            return _employeeAttachmentsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<EmployeeAttachmentDto> UpdateAsync(Guid id, EmployeeAttachmentUpdateDto input)
        {
            return _employeeAttachmentsAppService.UpdateAsync(id, input);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(EmployeeAttachmentExcelDownloadDto input)
        {
            return _employeeAttachmentsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _employeeAttachmentsAppService.GetDownloadTokenAsync();
        }
    }
}