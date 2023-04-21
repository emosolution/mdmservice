using DMSpro.OMS.MdmService.EmployeeProfiles;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using Volo.Abp.Domain.Entities;
namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
	public class SalesOrgEmpAssignmentWithDetailsDto: SalesOrgEmpAssignmentDto, IHasConcurrencyStamp

	{
        public SalesOrgHierarchyDto SalesOrgHierarchy { get; set; }
        public EmployeeProfileDto EmployeeProfile { get; set; }
        public SalesOrgEmpAssignmentWithDetailsDto()
		{
		}
	}
}

