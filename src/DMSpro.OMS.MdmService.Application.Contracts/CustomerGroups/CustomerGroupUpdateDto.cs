using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerGroups
{
    public class CustomerGroupUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(CustomerGroupConsts.CodeMaxLength, MinimumLength = CustomerGroupConsts.CodeMinLength)]
        public string Code { get; set; }
        [StringLength(CustomerGroupConsts.NameMaxLength)]
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public Type GroupBy { get; set; }
        public Status Status { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}