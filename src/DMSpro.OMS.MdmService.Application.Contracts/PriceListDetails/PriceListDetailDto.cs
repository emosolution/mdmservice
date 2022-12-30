using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
    public class PriceListDetailDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public int Price { get; set; }
        public int? BasedOnPrice { get; set; }
        public string Description { get; set; }
        public Guid PriceListId { get; set; }
        public Guid ItemMasterId { get; set; }
        public Guid UOMId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}