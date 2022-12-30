using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
    public class UOMGroupDetailDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public uint AltQty { get; set; }
        public uint BaseQty { get; set; }
        public bool Active { get; set; }
        public Guid UOMGroupId { get; set; }
        public Guid AltUOMId { get; set; }
        public Guid BaseUOMId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}