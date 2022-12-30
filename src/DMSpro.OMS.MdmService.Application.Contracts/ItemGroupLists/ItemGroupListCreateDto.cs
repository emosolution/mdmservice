using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    public class ItemGroupListCreateDto
    {
        public int Rate { get; set; } = 1;
        public Guid ItemGroupId { get; set; }
        public Guid ItemId { get; set; }
        public Guid UOMId { get; set; }
    }
}