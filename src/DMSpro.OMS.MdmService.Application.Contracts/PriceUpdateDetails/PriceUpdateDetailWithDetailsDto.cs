
using System;
using DMSpro.OMS.MdmService.PriceListDetails;
using DMSpro.OMS.MdmService.PriceUpdates;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
	public class PriceUpdateDetailWithDetailsDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public int PriceBeforeUpdate { get; set; }
        public int NewPrice { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid PriceUpdateId { get; set; }
        public Guid PriceListDetailId { get; set; }

        public string ConcurrencyStamp { get; set; }
        public PriceUpdateDto PriceUpdate { get; set; }
        public PriceListDetailDto PriceListDetail { get; set; }
        public PriceUpdateDetailWithDetailsDto()
		{
		}
	}
}

