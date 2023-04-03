using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{

    [Authorize(MdmServicePermissions.EmployeeProfiles.Default)]
    public partial class EmployeeProfilesAppService
    {
        public virtual async Task<EmployeeProfileDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<EmployeeProfile, EmployeeProfileDto>(await _employeeProfileRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.EmployeeProfiles.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _employeeProfileRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.EmployeeProfiles.Create)]
        public virtual async Task<EmployeeProfileDto> CreateAsync(EmployeeProfileCreateDto input)
        {
            await CheckCodeUniqueness(input.Code);
            CheckEffectiveDate(input.EffectiveDate, input.EndDate);
            var employeeProfile = await _employeeProfileManager.CreateAsync(
            input.WorkingPositionId, input.EmployeeTypeId, input.Code, input.ERPCode, input.FirstName, input.LastName, input.IdCardNumber, input.Email, input.Phone, input.Address, input.Active, input.EffectiveDate, input.DateOfBirth, input.EndDate, input.IdentityUserId
            );

            return ObjectMapper.Map<EmployeeProfile, EmployeeProfileDto>(employeeProfile);
        }

        [Authorize(MdmServicePermissions.EmployeeProfiles.Edit)]
        public virtual async Task<EmployeeProfileDto> UpdateAsync(Guid id, EmployeeProfileUpdateDto input)
        {
            await CheckCodeUniqueness(input.Code, id);
            CheckEffectiveDate(input.EffectiveDate, input.EndDate);
            var employeeProfile = await _employeeProfileManager.UpdateAsync(
            id,
            input.WorkingPositionId, input.EmployeeTypeId, input.Code, input.ERPCode, input.FirstName, input.LastName, input.IdCardNumber, input.Email, input.Phone, input.Address, input.Active, input.EffectiveDate, input.DateOfBirth, input.EndDate, input.IdentityUserId, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<EmployeeProfile, EmployeeProfileDto>(employeeProfile);
        }
    }
}