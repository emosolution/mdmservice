using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
	public partial class ItemAttribute
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new();
		}

		public List<string> GetNotNullProperty()
        {
            return new()
            {
                "AttrName",
            };
        }
    }
}