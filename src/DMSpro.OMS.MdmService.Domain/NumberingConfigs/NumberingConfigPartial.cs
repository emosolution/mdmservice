using DMSpro.OMS.MdmService.SystemDatas;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
	public partial class NumberingConfig
	{
        public virtual SystemData SystemData { get; set; }


        public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "SystemDataId", (1, "ISystemDataRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
			return new();
        }
    }
}