using JetBrains.Annotations;
using System;
using System.IO;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public class EmployeeProfileWithAvatarDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; }
        public string ERPCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string IdCardNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? IdentityUserId { get; set; }
        public EmployeeTypes? EmployeeType { get; set; }
        public Guid? WorkingPositionId { get; set; }
        [CanBeNull]
        public Stream Avatar { get; set; }   

        public string ConcurrencyStamp { get; set; }
        public EmployeeProfileWithAvatarDto()
		{
		}
    }
}