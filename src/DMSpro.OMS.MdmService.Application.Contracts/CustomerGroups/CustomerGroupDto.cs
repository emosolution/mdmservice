using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerGroups
{
    public class CustomerGroupDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public Type GroupBy { get; set; }
        public Status Status { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}