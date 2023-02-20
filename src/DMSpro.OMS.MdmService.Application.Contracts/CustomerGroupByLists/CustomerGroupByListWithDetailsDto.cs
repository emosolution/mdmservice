using System;
using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.Customers;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
	public class CustomerGroupByListWithDetailsDto: FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
	{
        public bool Active { get; set; }
        public Guid CustomerGroupId { get; set; }
        public Guid CustomerId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public CustomerGroupDto CustomerGroup { get; set; }
        public CustomerDto Customer { get; set; }

        public CustomerGroupByListWithDetailsDto()
		{
		}
	}
}

