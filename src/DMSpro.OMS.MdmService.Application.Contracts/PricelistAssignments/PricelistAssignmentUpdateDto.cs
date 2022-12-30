using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public class PricelistAssignmentUpdateDto : IHasConcurrencyStamp
    {
        public string Description { get; set; }
        public Guid PriceListId { get; set; }
        public Guid CustomerGroupId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}