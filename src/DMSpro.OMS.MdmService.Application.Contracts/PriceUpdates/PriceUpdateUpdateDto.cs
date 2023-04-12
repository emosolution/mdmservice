using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public class PriceUpdateUpdateDto : IHasConcurrencyStamp
    {
        [StringLength(PriceUpdateConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        //public DateTime? EffectiveDate { get; set; }
        //public DateTime? EndDate { get; set; }
        //public PriceUpdateStatus Status { get; set; }
        //public bool IsScheduled { get; set; }
        //public DateTime? ReleasedDate { get; set; }
        //public DateTime? CancelledDate { get; set; }
        //public DateTime? CompleteDate { get; set; }
        //public Guid PriceListId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}