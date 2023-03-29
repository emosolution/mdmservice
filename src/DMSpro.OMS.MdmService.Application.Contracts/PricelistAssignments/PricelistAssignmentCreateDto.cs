using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public class PricelistAssignmentCreateDto
    {
        public Guid PriceListId { get; set; }
        public Guid CustomerGroupId { get; set; }
    }
}