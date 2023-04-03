using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public class SalesOrgHierarchyDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool IsRoute { get; set; }
        public bool IsSellingZone { get; set; }
        public string HierarchyCode { get; set; }
        public bool Active { get; set; }
        public int DirectChildren { get; set; }
        public Guid SalesOrgHeaderId { get; set; }
        public Guid? ParentId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}