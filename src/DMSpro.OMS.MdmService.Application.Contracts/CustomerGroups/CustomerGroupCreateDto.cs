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
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime? EffectiveDate { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public Type GroupBy { get; set; } = ((Type[])Enum.GetValues(typeof(Type)))[0];
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public Status Status { get; set; } = ((Status[])Enum.GetValues(typeof(Status)))[0];
    }
}