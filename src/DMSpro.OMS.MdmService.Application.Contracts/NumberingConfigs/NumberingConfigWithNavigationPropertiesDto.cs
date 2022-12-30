using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.SystemDatas;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class NumberingConfigWithNavigationPropertiesDto
    {
        public NumberingConfigDto NumberingConfig { get; set; }

        public CompanyDto Company { get; set; }
        public SystemDataDto SystemData { get; set; }

    }
}