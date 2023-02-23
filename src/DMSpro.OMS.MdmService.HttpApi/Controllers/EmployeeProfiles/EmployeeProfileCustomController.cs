using DMSpro.OMS.MdmService.EmployeeProfiles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Controllers.EmployeeProfiles
{
    public partial class EmployeeProfileController
    {
        [HttpGet]
        [Route("employee-profile/{id}")]
        public async Task<EmployeeProfileFullDto> GetEmployeeProfile(Guid id)
        {
            return await _employeeProfilesAppService.GetEmployeeProfile(id);
        }
    }
}
