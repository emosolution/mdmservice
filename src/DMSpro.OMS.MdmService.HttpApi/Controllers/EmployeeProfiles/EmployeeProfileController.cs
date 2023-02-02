using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.EmployeeProfiles;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.EmployeeProfiles
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("EmployeeProfile")]
    [Route("api/mdm-service/employee-profiles")]
    public partial class EmployeeProfileController : AbpController, IEmployeeProfilesAppService
    {
        private readonly IEmployeeProfilesAppService _employeeProfilesAppService;

        public EmployeeProfileController(IEmployeeProfilesAppService employeeProfilesAppService)
        {
            _employeeProfilesAppService = employeeProfilesAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<EmployeeProfileWithNavigationPropertiesDto>> GetListAsync(GetEmployeeProfilesInput input)
        {
            return _employeeProfilesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<EmployeeProfileWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _employeeProfilesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<EmployeeProfileDto> GetAsync(Guid id)
        {
            return _employeeProfilesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("working-position-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetWorkingPositionLookupAsync(LookupRequestDto input)
        {
            return _employeeProfilesAppService.GetWorkingPositionLookupAsync(input);
        }

        [HttpGet]
        [Route("system-data-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetSystemDataLookupAsync(LookupRequestDto input)
        {
            return _employeeProfilesAppService.GetSystemDataLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<EmployeeProfileDto> CreateAsync(EmployeeProfileCreateDto input)
        {
            return _employeeProfilesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<EmployeeProfileDto> UpdateAsync(Guid id, EmployeeProfileUpdateDto input)
        {
            return _employeeProfilesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _employeeProfilesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(EmployeeProfileExcelDownloadDto input)
        {
            return _employeeProfilesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _employeeProfilesAppService.GetDownloadTokenAsync();
        }
    }
}