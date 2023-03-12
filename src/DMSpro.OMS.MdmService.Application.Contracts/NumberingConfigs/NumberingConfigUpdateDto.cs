using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class NumberingConfigUpdateDto : IHasConcurrencyStamp
    {
        [StringLength(NumberingConfigConsts.PrefixMaxLength)]
        public string Prefix { get; set; }
        [StringLength(NumberingConfigConsts.SuffixMaxLength)]
        public string Suffix { get; set; }
        [Range(NumberingConfigConsts.PaddingZeroNumberMinValue,
            NumberingConfigConsts.PaddingZeroNumberMaxValue)]
        public int? PaddingZeroNumber { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}