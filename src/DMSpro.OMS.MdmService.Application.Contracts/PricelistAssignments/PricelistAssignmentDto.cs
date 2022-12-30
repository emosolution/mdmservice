using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public class PricelistAssignmentDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Description { get; set; }
        public Guid PriceListId { get; set; }
        public Guid CustomerGroupId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}