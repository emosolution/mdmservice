using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerGroups
{
    public class CustomerGroupUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(CustomerGroupConsts.NameMaxLength, MinimumLength = CustomerGroupConsts.NameMinLength)]
        public string Name { get; set; }
        public bool Selectable { get; set; }
        public Type GroupBy { get; set; }
        [StringLength(CustomerGroupConsts.DescriptionMaxLength)]
        public string Description { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}