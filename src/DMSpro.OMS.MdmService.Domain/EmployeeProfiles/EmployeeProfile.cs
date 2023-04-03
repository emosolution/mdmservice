using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public partial class EmployeeProfile : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [CanBeNull]
        public virtual string ERPCode { get; set; }

        [NotNull]
        public virtual string FirstName { get; set; }

        [CanBeNull]
        public virtual string LastName { get; set; }

        public virtual DateTime? DateOfBirth { get; set; }

        [CanBeNull]
        public virtual string IdCardNumber { get; set; }

        [CanBeNull]
        public virtual string Email { get; set; }

        [CanBeNull]
        public virtual string Phone { get; set; }

        [CanBeNull]
        public virtual string Address { get; set; }

        public virtual bool Active { get; set; }

        public virtual DateTime EffectiveDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual Guid? IdentityUserId { get; set; }
        public Guid? WorkingPositionId { get; set; }
        public Guid? EmployeeTypeId { get; set; }

        public EmployeeProfile()
        {

        }

        public EmployeeProfile(Guid id, Guid? workingPositionId, Guid? employeeTypeId, string code, string erpCode, string firstName, string lastName, string idCardNumber, string email, string phone, string address, bool active, DateTime effectiveDate, DateTime? dateOfBirth = null, DateTime? endDate = null, Guid? identityUserId = null)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), EmployeeProfileConsts.CodeMaxLength, EmployeeProfileConsts.CodeMinLength);
            Check.Length(erpCode, nameof(erpCode), EmployeeProfileConsts.ERPCodeMaxLength, 0);
            Check.NotNull(firstName, nameof(firstName));
            Check.Length(firstName, nameof(firstName), EmployeeProfileConsts.FirstNameMaxLength, EmployeeProfileConsts.FirstNameMinLength);
            Check.Length(lastName, nameof(lastName), EmployeeProfileConsts.LastNameMaxLength, 0);
            Check.Length(idCardNumber, nameof(idCardNumber), EmployeeProfileConsts.IdCardNumberMaxLength, 0);
            Check.Length(email, nameof(email), EmployeeProfileConsts.EmailMaxLength, 0);
            Check.Length(phone, nameof(phone), EmployeeProfileConsts.PhoneMaxLength, 0);
            Check.Length(address, nameof(address), EmployeeProfileConsts.AddressMaxLength, 0);
            Code = code;
            ERPCode = erpCode;
            FirstName = firstName;
            LastName = lastName;
            IdCardNumber = idCardNumber;
            Email = email;
            Phone = phone;
            Address = address;
            Active = active;
            DateOfBirth = dateOfBirth;
            EffectiveDate = effectiveDate;
            EndDate = endDate;
            IdentityUserId = identityUserId;
            WorkingPositionId = workingPositionId;
            EmployeeTypeId = employeeTypeId;
        }

    }
}