using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Customers
{
	public partial class Customer
	{
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
                { "Attribute0Id", (1, "ICusAttributeValueRepository", "", "") },
                { "Attribute1Id", (1, "ICusAttributeValueRepository", "", "") },
                { "Attribute2Id", (1, "ICusAttributeValueRepository", "", "") },
                { "Attribute3Id", (1, "ICusAttributeValueRepository", "", "") },
                { "Attribute4Id", (1, "ICusAttributeValueRepository", "", "") },
                { "Attribute5Id", (1, "ICusAttributeValueRepository", "", "") },
                { "Attribute6Id", (1, "ICusAttributeValueRepository", "", "") },
                { "Attribute7Id", (1, "ICusAttributeValueRepository", "", "") },
                { "Attribute8Id", (1, "ICusAttributeValueRepository", "", "") },
                { "Attribute9Id", (1, "ICusAttributeValueRepository", "", "") },
                { "Attribute10Id", (1, "ICusAttributeValueRepository", "", "") },
                { "Attribute11Id", (1, "ICusAttributeValueRepository", "", "") },
                { "Attribute12Id", (1, "ICusAttributeValueRepository", "", "") },
                { "Attribute13Id", (1, "ICusAttributeValueRepository", "", "") },
                { "Attribute14Id", (1, "ICusAttributeValueRepository", "", "") },
                { "Attribute15Id", (1, "ICusAttributeValueRepository", "", "") },
                { "Attribute16Id", (1, "ICusAttributeValueRepository", "", "") },
                { "Attribute17Id", (1, "ICusAttributeValueRepository", "", "") },
                { "Attribute18Id", (1, "ICusAttributeValueRepository", "", "") },
                { "Attribute19Id", (1, "ICusAttributeValueRepository", "", "") },
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