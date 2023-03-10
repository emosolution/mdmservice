using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class NumberingConfigCreateDto
    {
        [StringLength(NumberingConfigConsts.PrefixMaxLength)]
        public string Prefix { get; set; }
        [StringLength(NumberingConfigConsts.SuffixMaxLength)]
        public string Suffix { get; set; }
        public int PaddingZeroNumber { get; set; }
        [StringLength(NumberingConfigConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public Guid? SystemDataId { get; set; }
    }
}