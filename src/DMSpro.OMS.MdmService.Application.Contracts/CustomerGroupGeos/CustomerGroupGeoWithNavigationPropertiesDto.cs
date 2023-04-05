using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.GeoMasters;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public class CustomerGroupGeoWithNavigationPropertiesDto
    {
        public CustomerGroupGeoDto CustomerGroupGeo { get; set; }

        public CustomerGroupDto CustomerGroup { get; set; }
        public GeoMasterDto GeoMaster { get; set; }
        public GeoMasterDto GeoMaster1 { get; set; }
        public GeoMasterDto GeoMaster2 { get; set; }
        public GeoMasterDto GeoMaster3 { get; set; }
        public GeoMasterDto GeoMaster4 { get; set; }

    }
}