using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.PriceLists;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
	public partial class PricelistAssignment : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual PriceList PriceList { get; set; }
        public virtual CustomerGroup CustomerGroup { get; set; }

        public Dictionary<string, (int, string, string, string)>
			GetExcelTemplateInfo()
		{
			return new()
			{
				{ "PriceListId", (1, "IPriceListRepository", "", "") },
                { "CustomerGroupId", (1, "ICustomerGroupRepository", "", "") },
            };
		}

		public List<string> GetNotNullProperty()
        {
			return new();
        }
    }
}