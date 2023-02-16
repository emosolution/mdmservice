using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Vendors
{
	public partial class Vendor
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "PriceListId", (1, "IPriceListRepository", "", "") },
                { "GeoMaster0Id", (1, "IGeoMasterRepository", "", "") },
                { "GeoMaster1Id", (1, "IGeoMasterRepository", "", "") },
                { "GeoMaster2Id", (1, "IGeoMasterRepository", "", "") },
                { "GeoMaster3Id", (1, "IGeoMasterRepository", "", "") },
                { "GeoMaster4Id", (1, "IGeoMasterRepository", "", "") },
                { "CompanyId", (1, "ICompanyRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
            return new()
            {
                "Code",
				"Name",
                "ShortName",
            };
        }
    }
}