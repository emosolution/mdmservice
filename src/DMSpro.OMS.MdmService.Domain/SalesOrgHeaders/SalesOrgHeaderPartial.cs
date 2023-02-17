using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
	public partial class SalesOrgHeader
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
                "Code",
            };
        }
    }
}