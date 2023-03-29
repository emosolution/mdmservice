using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.PriceLists
{
	public partial class PriceList
	{
        public virtual PriceList BasePriceList { get; set; }

        public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "BasePriceListId", (0, "IPriceListRepository", "", "") },
			};
		}

		public List<string> GetNotNullProperty()
        {
            return new()
            {
                "Code",
            };
        }
    }
}