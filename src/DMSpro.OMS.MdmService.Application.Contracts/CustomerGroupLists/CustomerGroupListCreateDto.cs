using System;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public class CustomerGroupListCreateDto
    {
        public Guid CustomerId { get; set; }
        public Guid CustomerGroupId { get; set; }
    }
}