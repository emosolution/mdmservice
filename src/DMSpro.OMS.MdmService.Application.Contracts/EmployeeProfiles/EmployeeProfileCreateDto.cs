using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public class EmployeeProfileCreateDto
    {
        [Required]
        [StringLength(EmployeeProfileConsts.CodeMaxLength, MinimumLength = EmployeeProfileConsts.CodeMinLength)]
        public string Code { get; set; }
        public string ERPCode { get; set; }
        [Required]
        [StringLength(EmployeeProfileConsts.FirstNameMaxLength, MinimumLength = EmployeeProfileConsts.FirstNameMinLength)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string IdCardNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; } = false;
        public DateTime? EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? IdentityUserId { get; set; }
        public Guid? WorkingPositionId { get; set; }
        public Guid? EmployeeTypeId { get; set; }
    }
}