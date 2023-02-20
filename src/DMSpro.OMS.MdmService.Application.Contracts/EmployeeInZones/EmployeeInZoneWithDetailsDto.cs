using System;
using DMSpro.OMS.MdmService.EmployeeProfiles;

namespace DMSpro.OMS.MdmService.EmployeeInZones
{
	public class EmployeeInZoneWithDetailsDto
	{
        public DateTime EffectiveDate { get; set; }
        public Guid? EndDate { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }
        public Guid EmployeeId { get; set; }

        public string ConcurrencyStamp { get; set; }
        public EmployeeProfileDto EmployeeProfile { get; set; }

        public EmployeeInZoneWithDetailsDto()
		{
		}
	}
}

