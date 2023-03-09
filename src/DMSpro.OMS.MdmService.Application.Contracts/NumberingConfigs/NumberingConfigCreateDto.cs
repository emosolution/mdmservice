using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class NumberingConfigCreateDto
    {
        public int StartNumber { get; set; }
        [StringLength(NumberingConfigConsts.PrefixMaxLength)]
        public string Prefix { get; set; }
        [StringLength(NumberingConfigConsts.SuffixMaxLength)]
        public string Suffix { get; set; }
        public int Length { get; set; }
        public bool Active { get; set; } = true;
        [StringLength(NumberingConfigConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool IsDefault { get; set; } = false;
        public Guid? SystemDataId { get; set; }
    }
}