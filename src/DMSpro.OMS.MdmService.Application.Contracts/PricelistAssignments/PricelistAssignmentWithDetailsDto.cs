using System;
using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.PriceLists;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
	public class PricelistAssignmentWithDetailsDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Description { get; set; }
        public Guid PriceListId { get; set; }
        public Guid CustomerGroupId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public PriceListDto priceList { get; set; }
        public CustomerGroupDto CustomerGroup { get; set; }
        public PricelistAssignmentWithDetailsDto()
		{
		}
	}
}

