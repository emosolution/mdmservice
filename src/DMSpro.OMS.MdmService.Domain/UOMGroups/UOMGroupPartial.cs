using DMSpro.OMS.MdmService.UOMGroupDetails;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.UOMGroups
{
	public partial class UOMGroup
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

        public virtual ICollection<UOMGroupDetail> Details { get; set; }
    }
}