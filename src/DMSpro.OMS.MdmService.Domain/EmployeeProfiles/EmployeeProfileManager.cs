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
        Guid? workingPositionId, Guid? employeeTypeId, string code, string erpCode, string firstName, string lastName, string idCardNumber, string email, string phone, string address, bool active, DateTime effectiveDate, DateTime? dateOfBirth = null, DateTime? endDate = null, Guid? identityUserId = null)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), EmployeeProfileConsts.CodeMaxLength, EmployeeProfileConsts.CodeMinLength);
            Check.Length(erpCode, nameof(erpCode), EmployeeProfileConsts.ERPCodeMaxLength);
            Check.NotNullOrWhiteSpace(firstName, nameof(firstName));
            Check.Length(firstName, nameof(firstName), EmployeeProfileConsts.FirstNameMaxLength, EmployeeProfileConsts.FirstNameMinLength);
            Check.Length(lastName, nameof(lastName), EmployeeProfileConsts.LastNameMaxLength);
            Check.Length(idCardNumber, nameof(idCardNumber), EmployeeProfileConsts.IdCardNumberMaxLength);
            Check.Length(email, nameof(email), EmployeeProfileConsts.EmailMaxLength);
            Check.Length(phone, nameof(phone), EmployeeProfileConsts.PhoneMaxLength);
            Check.Length(address, nameof(address), EmployeeProfileConsts.AddressMaxLength);
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var employeeProfile = new EmployeeProfile(
             GuidGenerator.Create(),
             workingPositionId, employeeTypeId, code, erpCode, firstName, lastName, idCardNumber, email, phone, address, active, effectiveDate, dateOfBirth, endDate, identityUserId
             );

            return await _employeeProfileRepository.InsertAsync(employeeProfile);
        }

        public async Task<EmployeeProfile> UpdateAsync(
            Guid id,
            Guid? workingPositionId, Guid? employeeTypeId, string code, string erpCode, string firstName, string lastName, string idCardNumber, string email, string phone, string address, bool active, DateTime effectiveDate, DateTime? dateOfBirth = null, DateTime? endDate = null, Guid? identityUserId = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), EmployeeProfileConsts.CodeMaxLength, EmployeeProfileConsts.CodeMinLength);
            Check.Length(erpCode, nameof(erpCode), EmployeeProfileConsts.ERPCodeMaxLength);
            Check.NotNullOrWhiteSpace(firstName, nameof(firstName));
            Check.Length(firstName, nameof(firstName), EmployeeProfileConsts.FirstNameMaxLength, EmployeeProfileConsts.FirstNameMinLength);
            Check.Length(lastName, nameof(lastName), EmployeeProfileConsts.LastNameMaxLength);
            Check.Length(idCardNumber, nameof(idCardNumber), EmployeeProfileConsts.IdCardNumberMaxLength);
            Check.Length(email, nameof(email), EmployeeProfileConsts.EmailMaxLength);
            Check.Length(phone, nameof(phone), EmployeeProfileConsts.PhoneMaxLength);
            Check.Length(address, nameof(address), EmployeeProfileConsts.AddressMaxLength);
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var employeeProfile = await _employeeProfileRepository.GetAsync(id);

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
            employeeProfile.EffectiveDate = effectiveDate;
            employeeProfile.DateOfBirth = dateOfBirth;
            employeeProfile.EndDate = endDate;
            employeeProfile.IdentityUserId = identityUserId;

            employeeProfile.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _employeeProfileRepository.UpdateAsync(employeeProfile);
        }

    }
}