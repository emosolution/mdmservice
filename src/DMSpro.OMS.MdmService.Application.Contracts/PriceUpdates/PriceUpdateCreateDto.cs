using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public class PriceUpdateCreateDto
    {
        [Required]
        [StringLength(PriceUpdateConsts.CodeMaxLength, MinimumLength = PriceUpdateConsts.CodeMinLength)]
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public PriceUpdateStatus Status { get; set; } = ((PriceUpdateStatus[])Enum.GetValues(typeof(PriceUpdateStatus)))[0];
        public DateTime? UpdateStatusDate { get; set; }
        public Guid PriceListId { get; set; }
    }
}