using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.ItemImages
{
    public class ItemImageDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public int DisplayOrder { get; set; }
        public Guid FileId { get; set; }
        public Guid ItemId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}