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
        public Guid? GeoMaster0Id { get; set; }
        public Guid? GeoMaster1Id { get; set; }
        public Guid? GeoMaster2Id { get; set; }
        public Guid? GeoMaster3Id { get; set; }
        public Guid? GeoMaster4Id { get; set; }
    }
}