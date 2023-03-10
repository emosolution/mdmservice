using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class NumberingConfigUpdateDto : IHasConcurrencyStamp
    {
        [StringLength(NumberingConfigConsts.PrefixMaxLength)]
        public string Prefix { get; set; }
        [StringLength(NumberingConfigConsts.SuffixMaxLength)]
        public string Suffix { get; set; }
        public int PaddingZeroNumber { get; set; }
        [StringLength(NumberingConfigConsts.DescriptionMaxLength)]

        public string ConcurrencyStamp { get; set; }
    }
}