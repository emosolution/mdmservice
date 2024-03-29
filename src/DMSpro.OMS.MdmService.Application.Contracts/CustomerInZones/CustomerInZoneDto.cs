using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerInZones
{
    public class CustomerInZoneDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }
        public Guid CustomerId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}