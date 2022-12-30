using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.GeoMasters;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Companies
{
    public class CompanyWithNavigationPropertiesDto
    {
        public CompanyDto Company { get; set; }

        public CompanyDto Company1 { get; set; }
        public GeoMasterDto GeoMaster { get; set; }
        public GeoMasterDto GeoMaster1 { get; set; }
        public GeoMasterDto GeoMaster2 { get; set; }
        public GeoMasterDto GeoMaster3 { get; set; }
        public GeoMasterDto GeoMaster4 { get; set; }

    }
}