using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
	public partial class EmployeeProfile
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "IdentityUserId", (2, "IdentityUsers", "GrpcRemotes:IdentiyServiceUrl", "DMSpro.OMS.Shared.Protos.IdentityService.IdentityUsers") },
                { "WorkingPositionId", (1, "IWorkingPositionRepository", "", "") },
                { "EmployeeTypeId", (1, "ISystemDataRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
            return new()
            {
                "Code",
                "FirstName",
            };
        }
    }
}