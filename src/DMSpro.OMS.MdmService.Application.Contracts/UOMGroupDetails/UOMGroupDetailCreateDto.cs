using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
    public class UOMGroupDetailCreateDto
    {
        public uint AltQty { get; set; }
        public uint BaseQty { get; set; }
        public bool Active { get; set; } = true;
        public Guid UOMGroupId { get; set; }
        public Guid AltUOMId { get; set; }
        public Guid BaseUOMId { get; set; }
    }
}