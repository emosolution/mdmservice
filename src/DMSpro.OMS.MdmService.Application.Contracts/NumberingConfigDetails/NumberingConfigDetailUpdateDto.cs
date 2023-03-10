using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public class NumberingConfigDetailUpdateDto : IHasConcurrencyStamp
    {
        [StringLength(NumberingConfigDetailConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        [StringLength(NumberingConfigDetailConsts.PrefixMaxLength)]
        public string Prefix { get; set; }
        public int PaddingZeroNumber { get; set; }
        [StringLength(NumberingConfigDetailConsts.SuffixMaxLength)]
        public string Suffix { get; set; }
        public bool Active { get; set; }
        public int CurrentNumber { get; set; }
        public Guid NumberingConfigId { get; set; }
        public Guid CompanyId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}