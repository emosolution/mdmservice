using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public partial class NumberingConfigDetail
    {
        public Dictionary<string, (int, string, string, string)>
             GetExcelTemplateInfo()
        {
            return new()
            {
                { "CompanyId", (1, "ICompanyRepository", "", "") },
                { "NumberingConfigId", (1, "INumberingConfigRepository", "", "") },
            };
        }

        public List<string> GetNotNullProperty()
        {
            return new();
        }
    }
}