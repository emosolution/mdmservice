using System;
using DMSpro.OMS.MdmService.EmployeeProfiles;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;

namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
	public class SalesOrgEmpAssignmentWithDetailsDto
	{
        public bool IsBase { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }
        public Guid EmployeeProfileId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public SalesOrgHierarchyDto SalesOrgHierarchy { get; set; }
        public EmployeeProfileDto EmployeeProfile { get; set; }
        public SalesOrgEmpAssignmentWithDetailsDto()
		{
		}
	}
}

