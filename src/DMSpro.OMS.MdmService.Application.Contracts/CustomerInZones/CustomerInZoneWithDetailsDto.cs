using System;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;

namespace DMSpro.OMS.MdmService.CustomerInZones
{
	public class CustomerInZoneWithDetailsDto
	{
        public DateTime? EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }
        public Guid CustomerId { get; set; }

        public string ConcurrencyStamp { get; set; }
        public CustomerDto Customer { get; set; }
        public SalesOrgHierarchyDto salesOrgHierarchy { get; set; }

        public CustomerInZoneWithDetailsDto()
		{
		}
	}
}

