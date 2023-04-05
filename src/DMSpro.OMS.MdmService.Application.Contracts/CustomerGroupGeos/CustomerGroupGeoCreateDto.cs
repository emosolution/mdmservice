using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public class CustomerGroupGeoCreateDto
    {
        [StringLength(CustomerGroupGeoConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool Active { get; set; } = true;
        public Guid CustomerGroupId { get; set; }
        public Guid GeoMaster0Id { get; set; }
        public Guid GeoMaster1Id { get; set; }
        public Guid GeoMaster2Id { get; set; }
        public Guid GeoMaster3Id { get; set; }
        public Guid GeoMaster4Id { get; set; }
    }
}