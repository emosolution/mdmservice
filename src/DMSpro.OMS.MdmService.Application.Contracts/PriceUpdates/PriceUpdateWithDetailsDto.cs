using DMSpro.OMS.MdmService.PriceLists;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
	public class PriceUpdateWithDetailsDto : PriceUpdateDto, IHasConcurrencyStamp
    {
        public PriceListDto PriceList { get; set; }

        public PriceUpdateWithDetailsDto()
		{
		}
	}
}

