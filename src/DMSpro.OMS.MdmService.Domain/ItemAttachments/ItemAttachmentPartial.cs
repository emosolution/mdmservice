using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
	public partial class ItemAttachment
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