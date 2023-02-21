using System;
using DMSpro.OMS.MdmService.UOMGroups;
using DMSpro.OMS.MdmService.UOMs;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
	public class UOMGroupDetailWithDetailsDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public uint AltQty { get; set; }
        public uint BaseQty { get; set; }
        public bool Active { get; set; }
        public Guid UOMGroupId { get; set; }
        public Guid AltUOMId { get; set; }
        public Guid BaseUOMId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public UOMGroupDto UOMGroup { get; set; }
        public UOMDto AltUOM { get; set; }
        public UOMDto BaseUOM { get; set; }

        public UOMGroupDetailWithDetailsDto()
		{
		}
	}
}

