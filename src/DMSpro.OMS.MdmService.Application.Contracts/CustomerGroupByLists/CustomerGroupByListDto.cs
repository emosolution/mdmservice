using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
    public class CustomerGroupByListDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public bool Active { get; set; }
        public Guid CustomerGroupId { get; set; }
        public Guid CustomerId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}