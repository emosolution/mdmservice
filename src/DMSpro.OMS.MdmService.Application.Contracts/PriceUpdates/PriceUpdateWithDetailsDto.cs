using System;
using DMSpro.OMS.MdmService.PriceLists;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
	public class PriceUpdateWithDetailsDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
        public PriceUpdateStatus Status { get; set; }
        public DateTime? UpdateStatusDate { get; set; }
        public Guid PriceListId { get; set; }

        public string ConcurrencyStamp { get; set; }
        public PriceListDto PriceList { get; set; }


        public PriceUpdateWithDetailsDto()
		{
		}
	}
}

