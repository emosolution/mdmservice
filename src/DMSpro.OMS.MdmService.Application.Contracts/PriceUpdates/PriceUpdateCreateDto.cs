using System;
using System.ComponentModel.DataAnnotations;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public class PriceUpdateCreateDto
    {
        [Required]
        [StringLength(PriceUpdateConsts.CodeMaxLength, MinimumLength = PriceUpdateConsts.CodeMinLength)]
        public string Code { get; set; }
        [StringLength(PriceUpdateConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        //public DateTime? EffectiveDate { get; set; }
        //public DateTime? EndDate { get; set; }
        //public PriceUpdateStatus Status { get; set; } = ((PriceUpdateStatus[])Enum.GetValues(typeof(PriceUpdateStatus)))[0];
        //public bool IsScheduled { get; set; } = false;
        //public DateTime? ReleasedDate { get; set; }
        //public DateTime? CancelledDate { get; set; }
        //public DateTime? CompleteDate { get; set; }
        public Guid PriceListId { get; set; }
    }
}