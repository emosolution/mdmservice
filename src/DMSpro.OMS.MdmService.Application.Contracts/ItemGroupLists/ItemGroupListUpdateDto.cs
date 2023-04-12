using System;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    public class ItemGroupListUpdateDto : IHasConcurrencyStamp
    {
        public Guid ItemId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}