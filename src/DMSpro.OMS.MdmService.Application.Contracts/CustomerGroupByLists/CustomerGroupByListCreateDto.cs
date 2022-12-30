using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
    public class CustomerGroupByListCreateDto
    {
        public bool Active { get; set; }
        public Guid CustomerGroupId { get; set; }
        public Guid CustomerId { get; set; }
    }
}