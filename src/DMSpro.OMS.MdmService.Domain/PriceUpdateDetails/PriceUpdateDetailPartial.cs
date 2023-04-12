using DMSpro.OMS.MdmService.PriceListDetails;
using DMSpro.OMS.MdmService.PriceUpdates;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
	public partial class PriceUpdateDetail
	{
        public virtual PriceUpdate PriceUpdate { get; set; }
        public virtual PriceListDetail PriceListDetail { get; set; }


        public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "PriceUpdateId", (1, "IPriceUpdateRepository", "", "") },
                { "PriceListDetailId", (1, "IPriceListDetailRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
			return new();
        }
    }
}