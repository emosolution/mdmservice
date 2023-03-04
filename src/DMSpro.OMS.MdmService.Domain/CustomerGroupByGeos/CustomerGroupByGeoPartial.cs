using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.GeoMasters;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
	public partial class CustomerGroupByGeo
	{
		public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "CustomerGroupId", (1, "ICustomerGroupRepository", "", "") },
                { "GeoMaster0Id", (1, "IGeoMasterRepository", "", "") },
                { "GeoMaster1Id", (1, "IGeoMasterRepository", "", "") },
                { "GeoMaster2Id", (1, "IGeoMasterRepository", "", "") },
                { "GeoMaster3Id", (1, "IGeoMasterRepository", "", "") },
                { "GeoMaster4Id", (1, "IGeoMasterRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
			return new();
        }

        public virtual CustomerGroup CustomerGroup { get; set; }
        public virtual GeoMaster GeoMaster0 { get; set; }
        public virtual GeoMaster GeoMaster1 { get; set; }
        public virtual GeoMaster GeoMaster2 { get; set; }
        public virtual GeoMaster GeoMaster3 { get; set; }
        public virtual GeoMaster GeoMaster4 { get; set; }
    }
}