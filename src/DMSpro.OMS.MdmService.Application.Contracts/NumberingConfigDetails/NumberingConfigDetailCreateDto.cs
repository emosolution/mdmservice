using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public class NumberingConfigDetailCreateDto
    {
        [StringLength(NumberingConfigDetailConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        [StringLength(NumberingConfigDetailConsts.PrefixMaxLength)]
        public string Prefix { get; set; }
        public int PaddingZeroNumber { get; set; } = 5;
        [StringLength(NumberingConfigDetailConsts.SuffixMaxLength)]
        public string Suffix { get; set; }
        public bool Active { get; set; } = true;
        public int CurrentNumber { get; set; } = 1;
        public Guid NumberingConfigId { get; set; }
        public Guid CompanyId { get; set; }
    }
}