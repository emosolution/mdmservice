using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using JetBrains.Annotations;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public class EmployeeProfileUpdateDto : IHasConcurrencyStamp
    {
        [StringLength(EmployeeProfileConsts.ERPCodeMaxLength)]
        public string ERPCode { get; set; }
        [Required]
        [StringLength(EmployeeProfileConsts.FirstNameMaxLength, MinimumLength = EmployeeProfileConsts.FirstNameMinLength)]
        public string FirstName { get; set; }
        [StringLength(EmployeeProfileConsts.LastNameMaxLength)]
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [StringLength(EmployeeProfileConsts.IdCardNumberMaxLength)]
        public string IdCardNumber { get; set; }
        [EmailAddress]
        [StringLength(EmployeeProfileConsts.EmailMaxLength)]
        public string Email { get; set; }
        [StringLength(EmployeeProfileConsts.PhoneMaxLength)]
        public string Phone { get; set; }
        [StringLength(EmployeeProfileConsts.AddressMaxLength)]
        public string Address { get; set; }
        public bool Active { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? IdentityUserId { get; set; }
        public EmployeeTypes? EmployeeType { get; set; }
        public Guid? WorkingPositionId { get; set; }
        [CanBeNull]
        public IRemoteStreamContent Avatar { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}