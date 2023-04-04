using DMSpro.OMS.MdmService.ItemAttributes;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
	public partial class ItemAttributeValue
	{
        public virtual ItemAttribute ItemAttribute { get; set; }
        public virtual ItemAttributeValue Parent { get; set; }

        public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "ParentId", (0, "IItemAttributeValueRepository", "", "") },
                { "ItemAttributeId", (1, "IItemAttributeRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
            return new()
            {
                "AttrValName",
            };
        }
    }
}