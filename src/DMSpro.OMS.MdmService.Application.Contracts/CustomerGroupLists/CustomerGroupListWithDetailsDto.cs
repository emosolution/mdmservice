using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.Customers;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public class CustomerGroupListWithDetailsDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CustomerGroupId { get; set; }

        public CustomerDto Customer { get; set; }

        public CustomerGroupDto CustomerGroup { get; set; }

        public string ConcurrencyStamp { get; set; }

        public CustomerGroupListWithDetailsDto()
        {

        }
    }
}