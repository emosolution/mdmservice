using DMSpro.OMS.MdmService.UOMGroupDetails;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.UOMGroups
{
    public class UOMGroupWithDetailsDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public List<UOMGroupDetailWithDetailsDto> Details { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
