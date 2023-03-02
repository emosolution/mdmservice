using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public class PricelistAssignmentCreateDto
    {
        [StringLength(PricelistAssignmentConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public Guid PriceListId { get; set; }
        public Guid CustomerGroupId { get; set; }
    }
}