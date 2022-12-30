using DMSpro.OMS.MdmService.WorkingPositions;
using DMSpro.OMS.MdmService.SystemDatas;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public class EmployeeProfile : FullAuditedAggregateRoot<Guid>, IMultiTenant
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

        public virtual DateTime? EffectiveDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual Guid? IdentityUserId { get; set; }
        public Guid? WorkingPositionId { get; set; }
        public Guid? EmployeeTypeId { get; set; }

        public EmployeeProfile()
        {

        }

        public EmployeeProfile(Guid id, Guid? workingPositionId, Guid? employeeTypeId, string code, string erpCode, string firstName, string lastName, string idCardNumber, string email, string phone, string address, bool active, DateTime? dateOfBirth = null, DateTime? effectiveDate = null, DateTime? endDate = null, Guid? identityUserId = null)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), EmployeeProfileConsts.CodeMaxLength, EmployeeProfileConsts.CodeMinLength);
            Check.NotNull(firstName, nameof(firstName));
            Check.Length(firstName, nameof(firstName), EmployeeProfileConsts.FirstNameMaxLength, EmployeeProfileConsts.FirstNameMinLength);
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