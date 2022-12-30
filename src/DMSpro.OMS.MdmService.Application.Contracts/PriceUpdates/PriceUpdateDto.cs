using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public class PriceUpdateDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public PriceUpdateStatus Status { get; set; }
        public DateTime? UpdateStatusDate { get; set; }
        public Guid PriceListId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}