using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.PriceLists;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Vendors
{
	public partial class Vendor
	{
        public virtual PriceList PriceList { get; set; }
        public virtual GeoMaster GeoMaster0 { get; set; }
        public virtual GeoMaster GeoMaster1 { get; set; }
        public virtual GeoMaster GeoMaster2 { get; set; }
        public virtual GeoMaster GeoMaster3 { get; set; }
        public virtual GeoMaster GeoMaster4 { get; set; }
        public virtual Company Company { get; set; }
        public virtual Company LinkedCompany { get; set; }

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
                { "LinkedCompanyId", (1, "ICompanyRepository", "", "") },
            };
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