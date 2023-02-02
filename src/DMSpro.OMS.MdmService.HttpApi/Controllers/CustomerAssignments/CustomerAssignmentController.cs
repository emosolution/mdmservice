using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CustomerAssignments;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;




namespace DMSpro.OMS.MdmService.Controllers.CustomerAssignments
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("CustomerAssignment")]
    [Route("api/mdm-service/customer-assignments")]
    public partial class CustomerAssignmentController : AbpController, ICustomerAssignmentsAppService
    {
        private readonly ICustomerAssignmentsAppService _customerAssignmentsAppService;

        public CustomerAssignmentController(ICustomerAssignmentsAppService customerAssignmentsAppService)
        {
            _customerAssignmentsAppService = customerAssignmentsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<CustomerAssignmentWithNavigationPropertiesDto>> GetListAsync(GetCustomerAssignmentsInput input)
        {
            return _customerAssignmentsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<CustomerAssignmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _customerAssignmentsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerAssignmentDto> GetAsync(Guid id)
        {
            return _customerAssignmentsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("company-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            return _customerAssignmentsAppService.GetCompanyLookupAsync(input);
        }

        [HttpGet]
        [Route("customer-profile-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input)
        {
            return _customerAssignmentsAppService.GetCustomerLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<CustomerAssignmentDto> CreateAsync(CustomerAssignmentCreateDto input)
        {
            return _customerAssignmentsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CustomerAssignmentDto> UpdateAsync(Guid id, CustomerAssignmentUpdateDto input)
        {
            return _customerAssignmentsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _customerAssignmentsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerAssignmentExcelDownloadDto input)
        {
            return _customerAssignmentsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _customerAssignmentsAppService.GetDownloadTokenAsync();
        }
    }
}