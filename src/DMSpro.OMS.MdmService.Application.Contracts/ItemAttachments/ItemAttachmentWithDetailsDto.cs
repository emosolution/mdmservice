using System;
using DMSpro.OMS.MdmService.Items;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
namespace DMSpro.OMS.MdmService.ItemAttachments
{
	public class ItemAttachmentWithDetailsDto: FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
	{
        public string Description { get; set; }
        public bool Active { get; set; }
        public Guid FileId { get; set; }
        public Guid ItemId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public ItemDto Item { get; set; }
        public ItemAttachmentWithDetailsDto()
		{
		}
	}
}

