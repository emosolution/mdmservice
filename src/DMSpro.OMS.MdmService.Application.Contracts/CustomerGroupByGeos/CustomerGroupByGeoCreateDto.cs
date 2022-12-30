using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
    public class CustomerGroupByGeoCreateDto
    {
        public bool Active { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public Guid CustomerGroupId { get; set; }
        public Guid GeoMasterId { get; set; }
    }
}