using System;
using DMSpro.OMS.MdmService.Items;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
namespace DMSpro.OMS.MdmService.ItemImages
{
	public class ItemImageWithDetailsDto: FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
	{
        public string Description { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
        public int DisplayOrder { get; set; }
        public Guid ItemId { get; set; }

        public string ConcurrencyStamp { get; set; }
        public ItemDto Item { get; set; }

        public ItemImageWithDetailsDto()
		{
		}
	}
}

