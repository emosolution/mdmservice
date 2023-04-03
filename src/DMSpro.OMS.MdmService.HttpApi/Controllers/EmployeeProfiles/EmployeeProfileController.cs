using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.EmployeeProfiles;

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
        [Route("{id}")]
        public virtual Task<EmployeeProfileDto> GetAsync(Guid id)
        {
            return _employeeProfilesAppService.GetAsync(id);
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
    }
}