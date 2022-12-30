using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public class GetEmployeeProfilesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Code { get; set; }
        public string ERPCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirthMin { get; set; }
        public DateTime? DateOfBirthMax { get; set; }
        public string IdCardNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool? Active { get; set; }
        public DateTime? EffectiveDateMin { get; set; }
        public DateTime? EffectiveDateMax { get; set; }
        public DateTime? EndDateMin { get; set; }
        public DateTime? EndDateMax { get; set; }
        public Guid? IdentityUserId { get; set; }
        public Guid? WorkingPositionId { get; set; }
        public Guid? EmployeeTypeId { get; set; }

        public GetEmployeeProfilesInput()
        {

        }
    }
}