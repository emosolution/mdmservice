using System;
using DMSpro.OMS.MdmService.EmployeeProfiles;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
	public class EmployeeImageWithDetailsDto
	{
        public string Description { get; set; }
        public string url { get; set; }
        public bool Active { get; set; }
        public bool IsAvatar { get; set; }
        public Guid EmployeeProfileId { get; set; }

        public string ConcurrencyStamp { get; set; }
        public EmployeeProfileDto EmployeeProfile { get; set; }

        public EmployeeImageWithDetailsDto()
		{
		}
	}
}

