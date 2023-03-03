using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.WeightMeasurements
{
	public partial class WeightMeasurement
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