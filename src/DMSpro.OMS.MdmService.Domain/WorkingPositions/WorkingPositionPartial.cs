using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.WorkingPositions
{
	public partial class WorkingPosition
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