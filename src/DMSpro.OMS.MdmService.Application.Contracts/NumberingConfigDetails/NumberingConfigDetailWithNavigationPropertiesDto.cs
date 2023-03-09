using DMSpro.OMS.MdmService.NumberingConfigs;
using DMSpro.OMS.MdmService.Companies;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public class NumberingConfigDetailWithNavigationPropertiesDto
    {
        public NumberingConfigDetailDto NumberingConfigDetail { get; set; }

        public NumberingConfigDto NumberingConfig { get; set; }
        public CompanyDto Company { get; set; }

    }
}