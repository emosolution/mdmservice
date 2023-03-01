using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
    public class CustomerGroupByAttCreateDto
    {
        [StringLength(CustomerGroupByAttConsts.ValueCodeMaxLength)]
        public string ValueCode { get; set; }
        [StringLength(CustomerGroupByAttConsts.ValueNameMaxLength)]
        public string ValueName { get; set; }
        public Guid CustomerGroupId { get; set; }
        public Guid CusAttributeValueId { get; set; }
    }
}