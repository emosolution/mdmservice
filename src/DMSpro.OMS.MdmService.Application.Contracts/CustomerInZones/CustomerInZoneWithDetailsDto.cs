using System;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
namespace DMSpro.OMS.MdmService.CustomerInZones
{
	public class CustomerInZoneWithDetailsDto: FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
	{
        public DateTime EffectiveDate { get; set; }
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

