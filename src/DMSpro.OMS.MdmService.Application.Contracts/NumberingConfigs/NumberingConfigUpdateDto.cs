using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class NumberingConfigUpdateDto : IHasConcurrencyStamp
    {
        public int StartNumber { get; set; }
        [StringLength(NumberingConfigConsts.PrefixMaxLength)]
        public string Prefix { get; set; }
        [StringLength(NumberingConfigConsts.SuffixMaxLength)]
        public string Suffix { get; set; }
        public int Length { get; set; }
        public bool Active { get; set; }
        [StringLength(NumberingConfigConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public Guid? SystemDataId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}