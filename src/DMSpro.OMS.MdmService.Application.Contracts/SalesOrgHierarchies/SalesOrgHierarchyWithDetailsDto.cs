using System;
using DMSpro.OMS.MdmService.SalesOrgHeaders;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
	public class SalesOrgHierarchyWithDetailsDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool IsRoute { get; set; }
        public bool IsSellingZone { get; set; }
        public string HierarchyCode { get; set; }
        public bool Active { get; set; }
        public Guid SalesOrgHeaderId { get; set; }
        public Guid? ParentId { get; set; }
        
        public string ConcurrencyStamp { get; set; }


        public SalesOrgHeaderDto SalesOrgHeader { get; set; }
        public SalesOrgHierarchyDto Parent { get; set; }
        public SalesOrgHierarchyWithDetailsDto()
		{
		}
	}
}

