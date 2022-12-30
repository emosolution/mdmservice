using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
    public class UOMGroupDetailUpdateDto : IHasConcurrencyStamp
    {
        public uint AltQty { get; set; }
        public uint BaseQty { get; set; }
        public bool Active { get; set; }
        public Guid UOMGroupId { get; set; }
        public Guid AltUOMId { get; set; }
        public Guid BaseUOMId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}