using System.Threading.Tasks;
using System;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public partial interface IEmployeeProfilesAppService
    {
        Task<EmployeeProfileFullDto> GetEmployeeProfile(Guid id);
    }
}
