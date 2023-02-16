using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
	public partial class CustomerAttribute
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