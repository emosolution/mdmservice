using System;
using DMSpro.OMS.MdmService.CusAttributeValues;
using DMSpro.OMS.MdmService.CustomerGroups;

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
	public class CustomerGroupByAttsWithDetailsDto
	{
        public string ValueCode { get; set; }
        public string ValueName { get; set; }
        public Guid CustomerGroupId { get; set; }
        public Guid CusAttributeValueId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public CustomerGroupDto CustomerGroup { get; set; }
        public CusAttributeValueDto CusAttributeValue { get; set; }

        public CustomerGroupByAttsWithDetailsDto()
		{
		}
	}
}

