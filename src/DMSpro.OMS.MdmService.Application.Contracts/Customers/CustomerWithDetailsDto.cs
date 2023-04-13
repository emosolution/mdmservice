using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.PriceLists;
using Volo.Abp.Domain.Entities;
namespace DMSpro.OMS.MdmService.Customers
{
	public class CustomerWithDetailsDto: CustomerDto, IHasConcurrencyStamp
	{
        public GeoMasterDto GeoMaster0 { get; set; }
        public GeoMasterDto GeoMaster1 { get; set; }
        public GeoMasterDto GeoMaster2 { get; set; }
        public GeoMasterDto GeoMaster3 { get; set; }
        public GeoMasterDto GeoMaster4 { get; set; }
        public PriceListDto PriceList { get; set; }
        public CustomerWithDetailsDto()
		{
		}
	}
}

