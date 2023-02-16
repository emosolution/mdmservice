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
				{ "", (0, "IRepository", "", "") },
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