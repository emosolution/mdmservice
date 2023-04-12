using System;
using System.ComponentModel.DataAnnotations;

namespace DMSpro.OMS.MdmService.CustomerGroups
{
    public class CustomerGroupCreateDto
    {
        [Required]
        [StringLength(CustomerGroupConsts.CodeMaxLength, MinimumLength = CustomerGroupConsts.CodeMinLength)]
        public string Code { get; set; }
        [Required]
        [StringLength(CustomerGroupConsts.NameMaxLength)]
        public string Name { get; set; }
        [StringLength(CustomerGroupConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool Selectable { get; set; }
        public Type GroupBy { get; set; } = ((Type[])Enum.GetValues(typeof(Type)))[0];
    }
}