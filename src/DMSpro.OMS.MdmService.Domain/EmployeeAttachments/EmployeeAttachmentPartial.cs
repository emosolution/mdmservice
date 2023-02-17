using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.EmployeeAttachments
{
	public partial class EmployeeAttachment
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "EmployeeProfileId", (1, "IEmployeeProfileRepository", "", "") },
			};
		}

		public List<string> GetNotNullProperty()
        {
            return new()
            {
                "url",
            };
        }
    }
}