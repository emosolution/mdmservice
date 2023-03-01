using System;
using DMSpro.OMS.MdmService.EmployeeProfiles;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
namespace DMSpro.OMS.MdmService.EmployeeInZones
{
	public class EmployeeInZoneWithDetailsDto: FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
	{
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }
        public Guid EmployeeId { get; set; }

        public string ConcurrencyStamp { get; set; }
        public EmployeeProfileDto EmployeeProfile { get; set; }

        public EmployeeInZoneWithDetailsDto()
		{
		}
	}
}

