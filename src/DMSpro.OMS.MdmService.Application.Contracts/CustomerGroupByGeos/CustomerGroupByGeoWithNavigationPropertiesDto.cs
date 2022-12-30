using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.GeoMasters;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
    public class CustomerGroupByGeoWithNavigationPropertiesDto
    {
        public CustomerGroupByGeoDto CustomerGroupByGeo { get; set; }

        public CustomerGroupDto CustomerGroup { get; set; }
        public GeoMasterDto GeoMaster { get; set; }

    }
}