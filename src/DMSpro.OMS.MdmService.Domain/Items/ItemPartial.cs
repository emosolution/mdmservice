using DMSpro.OMS.MdmService.UOMGroups;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.VATs;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Items
{
	public partial class Item
	{
        public virtual VAT VAT { get; set; }
        public virtual UOMGroup UOMGroup { get; set; }
        public virtual UOM InventoryUOM { get; set; }
        public virtual UOM PurUOM { get; set; }
        public virtual UOM SalesUOM { get; set; }

        public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
                { "VatId", (1, "IVATRepository", "", "") },
                { "UomGroupId", (1, "IUOMGroupRepository", "", "") },
                { "InventoryUOMId", (1, "IUOMRepository", "", "") },
                { "PurUOMId", (1, "IUOMRepository", "", "") },
                { "SalesUOMId", (1, "IUOMRepository", "", "") },
                { "Attr0Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr1Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr2Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr3Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr4Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr5Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr6Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr7Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr8Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr9Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr10Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr11Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr12Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr13Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr14Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr15Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr16Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr17Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr18Id", (1, "IItemAttributeValueRepository", "", "") },
                { "Attr19Id", (1, "IItemAttributeValueRepository", "", "") },
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