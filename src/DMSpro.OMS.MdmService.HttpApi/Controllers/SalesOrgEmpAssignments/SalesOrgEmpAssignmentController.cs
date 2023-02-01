using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.SalesOrgEmpAssignments;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Controllers.SalesOrgEmpAssignments
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("SalesOrgEmpAssignment")]
    [Route("api/mdm-service/sales-org-emp-assignments")]
    public partial class SalesOrgEmpAssignmentController : AbpController, ISalesOrgEmpAssignmentsAppService
    {
        private readonly ISalesOrgEmpAssignmentsAppService _salesOrgEmpAssignmentsAppService;

        public SalesOrgEmpAssignmentController(ISalesOrgEmpAssignmentsAppService salesOrgEmpAssignmentsAppService)
        {
            _salesOrgEmpAssignmentsAppService = salesOrgEmpAssignmentsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<SalesOrgEmpAssignmentWithNavigationPropertiesDto>> GetListAsync(GetSalesOrgEmpAssignmentsInput input)
        {
            return _salesOrgEmpAssignmentsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<SalesOrgEmpAssignmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _salesOrgEmpAssignmentsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<SalesOrgEmpAssignmentDto> GetAsync(Guid id)
        {
            return _salesOrgEmpAssignmentsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("sales-org-hierarchy-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input)
        {
            return _salesOrgEmpAssignmentsAppService.GetSalesOrgHierarchyLookupAsync(input);
        }

        [HttpGet]
        [Route("employee-profile-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeProfileLookupAsync(LookupRequestDto input)
        {
            return _salesOrgEmpAssignmentsAppService.GetEmployeeProfileLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<SalesOrgEmpAssignmentDto> CreateAsync(SalesOrgEmpAssignmentCreateDto input)
        {
            return _salesOrgEmpAssignmentsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<SalesOrgEmpAssignmentDto> UpdateAsync(Guid id, SalesOrgEmpAssignmentUpdateDto input)
        {
            return _salesOrgEmpAssignmentsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _salesOrgEmpAssignmentsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(SalesOrgEmpAssignmentExcelDownloadDto input)
        {
            return _salesOrgEmpAssignmentsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _salesOrgEmpAssignmentsAppService.GetDownloadTokenAsync();
        }
    }
}