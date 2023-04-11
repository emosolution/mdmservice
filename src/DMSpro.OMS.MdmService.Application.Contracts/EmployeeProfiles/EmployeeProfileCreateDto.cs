using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Content;
using JetBrains.Annotations;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public class EmployeeProfileCreateDto
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
        public bool Active { get; set; } = true;
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? IdentityUserId { get; set; }
        public EmployeeTypes? EmployeeType { get; set; }
        public Guid? WorkingPositionId { get; set; }
        [CanBeNull]
        public IRemoteStreamContent Avatar { get; set; }
    }
}