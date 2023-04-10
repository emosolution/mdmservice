using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
    public class CustomerAttributeUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(CustomerAttributeConsts.AttrNameMaxLength, MinimumLength = CustomerAttributeConsts.AttrNameMinLength)]
        public string AttrName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}