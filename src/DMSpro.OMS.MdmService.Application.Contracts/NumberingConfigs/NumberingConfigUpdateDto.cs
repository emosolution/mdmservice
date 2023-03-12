using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class NumberingConfigUpdateDto : IHasConcurrencyStamp
    {
        [StringLength(NumberingConfigConsts.PrefixMaxLength)]
        [NotNull]
        public string Prefix { get; set; }
        [StringLength(NumberingConfigConsts.SuffixMaxLength)]
        [NotNull]
        public string Suffix { get; set; }
        public int PaddingZeroNumber { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}