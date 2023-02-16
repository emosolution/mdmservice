using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
	public partial class PriceUpdateDetail
	{
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