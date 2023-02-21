using System;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.MCPDetails;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.VisitPlans
{
	public class VisitPlanWithDetailsDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public DateTime DateVisit { get; set; }
        public int Distance { get; set; }
        public int VisitOrder { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public DayOfWeek DayOfWeek { get; set; }
        public int Week { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public Guid MCPDetailId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid RouteId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid? ItemGroupId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public MCPDetailDto MCPDetail { get; set; }
        public CustomerDto Customer { get; set; }
        public SalesOrgHierarchyDto Route { get; set; }
        public ItemGroupDto ItemGroup { get; set; }

        public VisitPlanWithDetailsDto()
		{
		}
	}
}

