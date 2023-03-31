using System;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
namespace DMSpro.OMS.MdmService.CompanyInZones
{
	public class CompanyInZoneWithDetailsDto: FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
	{
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid ItemGroupId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public CompanyDto Company { get; set; }
        public SalesOrgHierarchyDto SalesOrgHierarchy { get; set; }
        public ItemGroupDto ItemGroup { get; set; }

        public CompanyInZoneWithDetailsDto()
		{
		}
	}
}

