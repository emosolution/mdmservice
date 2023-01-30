using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public class EmployeeProfilesInternalAppService : ApplicationService, IEmployeeProfilesInternalAppService
    {
        private readonly IEmployeeProfileRepository _repository;

        public EmployeeProfilesInternalAppService(IEmployeeProfileRepository repository)
        {
            _repository = repository;
        }

        public virtual async Task<EmployeeProfileWithTenantDto> GetWithTenantIdAsynce(Guid id)
        {
            try
            {
                EmployeeProfile result = await _repository.GetAsync(id);
                return ObjectMapper.Map<EmployeeProfile, EmployeeProfileWithTenantDto>(result);
            }
            catch (EntityNotFoundException)
            {
                return null;
            }
        }
    }
}
