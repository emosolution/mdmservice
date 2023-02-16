using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
	public partial class UOMGroupDetail
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "UOMGroupId", (1, "IUOMGroupRepository", "", "") },
                { "AltUOMId", (1, "IUOMRepository", "", "") },
                { "BaseUOMId", (1, "IUOMRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
			return new();
        }
    }
}