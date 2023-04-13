using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public class PriceUpdateDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; }
        public string Description { get; set; }
        //public DateTime? EffectiveDate { get; set; }
        //public DateTime? EndDate { get; set; }
        public PriceUpdateStatus Status { get; set; }
        //public bool IsScheduled { get; set; }
        //public DateTime? ReleasedDate { get; set; }
        public DateTime? CancelledDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public Guid PriceListId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}