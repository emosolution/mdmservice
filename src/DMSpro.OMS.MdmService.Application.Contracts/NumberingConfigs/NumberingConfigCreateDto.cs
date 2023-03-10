using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class NumberingConfigCreateDto
    {
        [StringLength(NumberingConfigConsts.PrefixMaxLength)]
        [CanBeNull]
        public string Prefix { get; set; }
        [StringLength(NumberingConfigConsts.SuffixMaxLength)]
        [CanBeNull]
        public string Suffix { get; set; }
        public int? PaddingZeroNumber { get; set; }
        [NotNull]
        public string ObjectType { get; set; }
    }
}