using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
	public partial class CompanyIdentityUserAssignment
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "CompanyId", (1, "ICompanyRepository", "", "") },
                { "IdentityUserId", (2, "IdentityUsers", "GrpcRemotes:IdentiyServiceUrl", "DMSpro.OMS.Shared.Protos.IdentityService.IdentityUsers") },
            };
		}

		public List<string> GetNotNullProperty()
        {
			return new();
        }
    }
}