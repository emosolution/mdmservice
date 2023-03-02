using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.ItemImages
{
    public class ItemImageUpdateDto : IHasConcurrencyStamp
    {
        [StringLength(ItemImageConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool Active { get; set; }
        public int DisplayOrder { get; set; } = 0;
        [Required]
        public IRemoteStreamContent File { get; set; }
        [Required]
        public Guid ItemId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}