using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public class PriceUpdateUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(PriceUpdateConsts.CodeMaxLength, MinimumLength = PriceUpdateConsts.CodeMinLength)]
        public string Code { get; set; }
        [StringLength(PriceUpdateConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
        public PriceUpdateStatus Status { get; set; }
        public DateTime? UpdateStatusDate { get; set; }
        public Guid PriceListId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}