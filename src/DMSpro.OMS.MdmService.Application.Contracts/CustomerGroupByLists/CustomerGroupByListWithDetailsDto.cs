using System;
using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.Customers;

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
	public class CustomerGroupByListWithDetailsDto
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

