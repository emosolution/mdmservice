using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public class CustomerGroupListCreateDto
    {
        [StringLength(CustomerGroupListConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool Active { get; set; } = true;
        public Guid CustomerId { get; set; }
        public Guid CustomerGroupId { get; set; }
    }
}