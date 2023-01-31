using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public interface IEmployeeProfilesInternalAppService : IApplicationService
    {
        Task<EmployeeProfileWithTenantDto> GetWithTenantIdAsynce(Guid id);
    }
}
