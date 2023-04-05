using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public class ItemAttributeValueUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(ItemAttributeValueConsts.AttrValNameMaxLength, MinimumLength = ItemAttributeValueConsts.AttrValNameMinLength)]
        public string AttrValName { get; set; }
        
        public string ConcurrencyStamp { get; set; }
    }
}