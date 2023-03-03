using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerImages
{
    public class CustomerImageDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool IsAvatar { get; set; }
        public bool IsPOSM { get; set; }
        public Guid FileId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? POSMItemId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}