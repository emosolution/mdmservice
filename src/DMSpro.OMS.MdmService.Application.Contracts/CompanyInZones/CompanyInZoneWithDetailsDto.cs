using System;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;

namespace DMSpro.OMS.MdmService.CompanyInZones
{
	public class CompanyInZoneWithDetailsDto
	{
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsBase { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }
        public Guid CompanyId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public CompanyDto Company { get; set; }
        public SalesOrgHierarchyDto SalesOrgHierarchy { get; set; }
        public CompanyInZoneWithDetailsDto()
		{
		}
	}
}

