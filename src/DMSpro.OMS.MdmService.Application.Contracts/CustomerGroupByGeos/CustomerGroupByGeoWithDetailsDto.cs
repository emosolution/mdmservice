using System;
using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.GeoMasters;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
	public class CustomerGroupByGeoWithDetailsDto: FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
	{
        public bool Active { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public Guid CustomerGroupId { get; set; }
        public Guid GeoMasterId { get; set; }

        public string ConcurrencyStamp { get; set; }
        public CustomerGroupDto CustomerGroup { get; set; }
        public GeoMasterDto GeoMaster { get; set; }

        public CustomerGroupByGeoWithDetailsDto()
		{
		}
	}
}

