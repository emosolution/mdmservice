using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.DimensionMeasurements
{
	public partial class DimensionMeasurement
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
				"Name",
            };
        }
    }
}