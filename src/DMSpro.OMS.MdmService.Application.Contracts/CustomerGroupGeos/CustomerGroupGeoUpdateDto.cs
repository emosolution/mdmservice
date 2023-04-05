using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public class CustomerGroupGeoUpdateDto : IHasConcurrencyStamp
    {
        [StringLength(CustomerGroupGeoConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool Active { get; set; }
        public Guid CustomerGroupId { get; set; }
        public Guid GeoMaster0Id { get; set; }
        public Guid? GeoMaster1Id { get; set; }
        public Guid? GeoMaster2Id { get; set; }
        public Guid? GeoMaster3Id { get; set; }
        public Guid? GeoMaster4Id { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}