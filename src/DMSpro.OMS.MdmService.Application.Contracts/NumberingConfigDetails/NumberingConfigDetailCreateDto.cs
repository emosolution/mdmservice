using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public class NumberingConfigDetailCreateDto
    {
        public bool Active { get; set; } = true;
        [StringLength(NumberingConfigDetailConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public Guid NumberingConfigId { get; set; }
        public Guid CompanyId { get; set; }
    }
}