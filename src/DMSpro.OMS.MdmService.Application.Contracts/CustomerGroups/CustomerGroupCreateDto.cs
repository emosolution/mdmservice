using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroups
{
    public class CustomerGroupCreateDto
    {
        [Required]
        [StringLength(CustomerGroupConsts.CodeMaxLength, MinimumLength = CustomerGroupConsts.CodeMinLength)]
        public string Code { get; set; }
        [StringLength(CustomerGroupConsts.NameMaxLength)]
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public Type GroupBy { get; set; } = ((Type[])Enum.GetValues(typeof(Type)))[0];
        public Status Status { get; set; } = ((Status[])Enum.GetValues(typeof(Status)))[0];
    }
}