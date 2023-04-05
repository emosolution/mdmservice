using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.PriceLists;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Customers
{
	public partial class Customer
	{
        public virtual GeoMaster GeoMaster0 { get; set; }
        public virtual GeoMaster GeoMaster1 { get; set; }
        public virtual GeoMaster GeoMaster2 { get; set; }
        public virtual GeoMaster GeoMaster3 { get; set; }
        public virtual GeoMaster GeoMaster4 { get; set; }
        
        [CanBeNull]
        public virtual PriceList PriceList { get; set; }
        //public virtual Company LinkedCompany { get; set; }

        public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
                //TODO Add Warehouse GRPC Info
				//{ "WarehouseId", (2, "WarehouseServices", "", "") },
                { "PaymentTermId", (1, "ISystemDataRepository", "", "") },
                { "LinkedCompanyId", (1, "ICompanyRepository", "", "") },
                { "PriceListId", (1, "IPriceListRepository", "", "") },
                { "GeoMaster0Id", (1, "IGeoMasterRepository", "", "") },
                { "GeoMaster1Id", (1, "IGeoMasterRepository", "", "") },
                { "GeoMaster2Id", (1, "IGeoMasterRepository", "", "") },
                { "GeoMaster3Id", (1, "IGeoMasterRepository", "", "") },
                { "GeoMaster4Id", (1, "IGeoMasterRepository", "", "") },
                { "Attr0Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr1Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr2Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr3Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr4Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr5Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr6Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr7Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr8Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr9Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr10Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr11Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr12Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr13Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr14Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr15Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr16Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr17Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr18Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "Attr19Id", (1, "ICustomerAttributeValueRepository", "", "") },
                { "PaymentId", (0, "ICustomerRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
            return new()
            {
                "Code",
            };
        }
    }
}