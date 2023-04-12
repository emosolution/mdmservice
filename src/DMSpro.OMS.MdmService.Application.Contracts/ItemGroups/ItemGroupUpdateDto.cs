using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.ItemGroups
{
    public class ItemGroupUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(ItemGroupConsts.NameMaxLength)]
        public string Name { get; set; }
        [StringLength(ItemGroupConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public GroupType Type { get; set; }
        public bool? Selectable { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}