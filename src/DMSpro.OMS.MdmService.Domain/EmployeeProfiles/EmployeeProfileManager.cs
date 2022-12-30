using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public class EmployeeProfileManager : DomainService
    {
        private readonly IEmployeeProfileRepository _employeeProfileRepository;

        public EmployeeProfileManager(IEmployeeProfileRepository employeeProfileRepository)
        {
            _employeeProfileRepository = employeeProfileRepository;
        }

        public async Task<EmployeeProfile> CreateAsync(
        Guid? workingPositionId, Guid? employeeTypeId, string code, string erpCode, string firstName, string lastName, string idCardNumber, string email, string phone, string address, bool active, DateTime? dateOfBirth = null, DateTime? effectiveDate = null, DateTime? endDate = null, Guid? identityUserId = null)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), EmployeeProfileConsts.CodeMaxLength, EmployeeProfileConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(firstName, nameof(firstName));
            Check.Length(firstName, nameof(firstName), EmployeeProfileConsts.FirstNameMaxLength, EmployeeProfileConsts.FirstNameMinLength);

            var employeeProfile = new EmployeeProfile(
             GuidGenerator.Create(),
             workingPositionId, employeeTypeId, code, erpCode, firstName, lastName, idCardNumber, email, phone, address, active, dateOfBirth, effectiveDate, endDate, identityUserId
             );

            return await _employeeProfileRepository.InsertAsync(employeeProfile);
        }

        public async Task<EmployeeProfile> UpdateAsync(
            Guid id,
            Guid? workingPositionId, Guid? employeeTypeId, string code, string erpCode, string firstName, string lastName, string idCardNumber, string email, string phone, string address, bool active, DateTime? dateOfBirth = null, DateTime? effectiveDate = null, DateTime? endDate = null, Guid? identityUserId = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), EmployeeProfileConsts.CodeMaxLength, EmployeeProfileConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(firstName, nameof(firstName));
            Check.Length(firstName, nameof(firstName), EmployeeProfileConsts.FirstNameMaxLength, EmployeeProfileConsts.FirstNameMinLength);

            var queryable = await _employeeProfileRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var employeeProfile = await AsyncExecuter.FirstOrDefaultAsync(query);

            employeeProfile.WorkingPositionId = workingPositionId;
            employeeProfile.EmployeeTypeId = employeeTypeId;
            employeeProfile.Code = code;
            employeeProfile.ERPCode = erpCode;
            employeeProfile.FirstName = firstName;
            employeeProfile.LastName = lastName;
            employeeProfile.IdCardNumber = idCardNumber;
            employeeProfile.Email = email;
            employeeProfile.Phone = phone;
            employeeProfile.Address = address;
            employeeProfile.Active = active;
            employeeProfile.DateOfBirth = dateOfBirth;
            employeeProfile.EffectiveDate = effectiveDate;
            employeeProfile.EndDate = endDate;
            employeeProfile.IdentityUserId = identityUserId;

            employeeProfile.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _employeeProfileRepository.UpdateAsync(employeeProfile);
        }

    }
}