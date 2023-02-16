using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
	public partial class PriceListDetail
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "PriceListId", (1, "IPriceListRepository", "", "") },
                { "UOMId", (1, "IUOMRepository", "", "") },
                { "ItemId", (1, "IItemRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
            return new()
            {
                "Description",
            };
        }
    }
}