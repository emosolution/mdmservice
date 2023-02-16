using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemImages
{
	public partial class ItemImage
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "ItemId", (1, "IItemRepository", "", "") },
			};
		}

		public List<string> GetNotNullProperty()
        {
            return new()
            {
                "Url",
            };
        }
    }
}