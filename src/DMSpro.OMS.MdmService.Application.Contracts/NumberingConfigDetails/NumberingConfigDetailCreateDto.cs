using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using DMSpro.OMS.MdmService.NumberingConfigs;
using JetBrains.Annotations;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public class NumberingConfigDetailCreateDto
    {
        [StringLength(NumberingConfigConsts.PrefixMaxLength)]
        [CanBeNull]
        public string Prefix { get; set; }
        [Range(NumberingConfigConsts.PaddingZeroNumberMinValue,
            NumberingConfigConsts.PaddingZeroNumberMaxValue)]
        public int? PaddingZeroNumber { get; set; }
        [StringLength(NumberingConfigConsts.SuffixMaxLength)]
        [CanBeNull]
        public string Suffix { get; set; }
        public bool? Active { get; set; }
        [Range(NumberingConfigDetailConsts.CurrentNumberMinValue,
            double.MaxValue)]
        public int CurrentNumber { get; set; }
        [NotNull]
        public Guid NumberingConfigId { get; set; }
        [NotNull]
        public Guid CompanyId { get; set; }
    }
}