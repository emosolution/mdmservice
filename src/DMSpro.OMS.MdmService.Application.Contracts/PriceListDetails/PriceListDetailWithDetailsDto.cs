using System;
using DMSpro.OMS.MdmService.Items;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.UOMs;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
	public class PriceListDetailWithDetailsDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public decimal Price { get; set; }
        public decimal? BasedOnPrice { get; set; }
        public string Description { get; set; }
        public Guid PriceListId { get; set; }
        public Guid UOMId { get; set; }
        public Guid ItemId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public PriceListDto PriceList { get; set; }
        public UOMDto UOM { get; set;}
        public ItemDto Item { get; set; }

        public PriceListDetailWithDetailsDto()
		{
		}
	}
}

