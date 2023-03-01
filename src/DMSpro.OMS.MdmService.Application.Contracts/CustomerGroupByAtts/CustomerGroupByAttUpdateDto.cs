using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
    public class CustomerGroupByAttUpdateDto : IHasConcurrencyStamp
    {
        [StringLength(CustomerGroupByAttConsts.ValueCodeMaxLength)]
        public string ValueCode { get; set; }
        [StringLength(CustomerGroupByAttConsts.ValueNameMaxLength)]
        public string ValueName { get; set; }
        public Guid CustomerGroupId { get; set; }
        public Guid CusAttributeValueId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}