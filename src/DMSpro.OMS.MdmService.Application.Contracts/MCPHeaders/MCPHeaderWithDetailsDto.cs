using System;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.ItemGroups;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
	public class MCPHeaderWithDetailsDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid RouteId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid? ItemGroupId { get; set; }

        public string ConcurrencyStamp { get; set; }
        public CompanyDto Company { get; set; }
        public ItemGroupDto ItemGroup { get; set; }
        public MCPHeaderWithDetailsDto()
		{
		}
	}
}

