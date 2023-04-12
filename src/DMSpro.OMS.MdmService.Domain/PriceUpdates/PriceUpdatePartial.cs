using DMSpro.OMS.MdmService.PriceLists;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public partial class PriceUpdate
    {
        public virtual PriceList PriceList { get; set; }

        public Dictionary<string, (int, string, string, string)>
            GetExcelTemplateInfo()
        {
            return new()
            {
                { "PriceListId", (1, "IPriceListRepository", "", "") },
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