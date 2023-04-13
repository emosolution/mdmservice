using System;
using System.ComponentModel.DataAnnotations;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public class CustomerGroupGeoCreateDto
    {
        public Guid CustomerGroupId { get; set; }
        public Guid GeoMaster0Id { get; set; }
        public Guid? GeoMaster1Id { get; set; }
        public Guid? GeoMaster2Id { get; set; }
        public Guid? GeoMaster3Id { get; set; }
        public Guid? GeoMaster4Id { get; set; }
    }
}