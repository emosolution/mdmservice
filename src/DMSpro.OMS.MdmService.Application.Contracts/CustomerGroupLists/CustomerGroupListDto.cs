using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public class CustomerGroupListDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CustomerGroupId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}