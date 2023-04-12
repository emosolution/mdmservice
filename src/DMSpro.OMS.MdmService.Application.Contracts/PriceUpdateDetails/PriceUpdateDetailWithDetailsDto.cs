using DMSpro.OMS.MdmService.PriceListDetails;
using DMSpro.OMS.MdmService.PriceUpdates;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
	public class PriceUpdateDetailWithDetailsDto : PriceUpdateDetailDto, IHasConcurrencyStamp
    {
        public PriceUpdateDto PriceUpdate { get; set; }
        public PriceListDetailDto PriceListDetail { get; set; }
        public PriceUpdateDetailWithDetailsDto()
		{
		}
	}
}

