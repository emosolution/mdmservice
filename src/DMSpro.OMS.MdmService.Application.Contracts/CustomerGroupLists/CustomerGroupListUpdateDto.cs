using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public class CustomerGroupListUpdateDto : IHasConcurrencyStamp
    {
        [StringLength(CustomerGroupListConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool Active { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CustomerGroupId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}