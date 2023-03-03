using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Companies
{
    public partial class Company
    {
        public Dictionary<string, (int, string, string, string)> GetExcelTemplateInfo()
        {
            return new()
            {
                { "ParentId", (0, "ICompanyRepository", "", "") },
                { "GeoLevel0Id", (1, "IGeoMasterRepository", "", "") },
                { "GeoLevel1Id", (1, "IGeoMasterRepository", "", "") },
                { "GeoLevel2Id", (1, "IGeoMasterRepository", "", "") },
                { "GeoLevel3Id", (1, "IGeoMasterRepository", "", "") },
                { "GeoLevel4Id", (1, "IGeoMasterRepository", "", "") },
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
