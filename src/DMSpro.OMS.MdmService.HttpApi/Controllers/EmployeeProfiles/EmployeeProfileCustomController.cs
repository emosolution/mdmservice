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
        public virtual Task<EmployeeProfileFullDto> GetEmployeeProfileAsync(Guid id)
        {
            return _employeeProfilesAppService.GetEmployeeProfileAsync(id);
        }

        [HttpGet]
        [Route("with-avatar/{id}")]
        public virtual Task<EmployeeProfileWithAvatarDto> GetWithAvatarAsync(Guid id)
        {
            return _employeeProfilesAppService.GetWithAvatarAsync(id);
        }
    }
}
