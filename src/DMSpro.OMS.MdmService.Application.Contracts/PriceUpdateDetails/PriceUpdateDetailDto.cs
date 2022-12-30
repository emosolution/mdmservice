using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
    public class PriceUpdateDetailDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public int PriceBeforeUpdate { get; set; }
        public int NewPrice { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid PriceUpdateId { get; set; }
        public Guid PriceListDetailId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}