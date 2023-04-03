using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.WorkingPositions;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
	public partial class EmployeeProfile
	{
        public virtual WorkingPosition WorkingPosition { get; set; }
        public virtual SystemData EmployeeType { get; set; }

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