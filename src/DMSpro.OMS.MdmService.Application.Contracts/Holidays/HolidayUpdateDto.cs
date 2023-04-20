using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.Holidays
{
    public class HolidayUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [Range(HolidayConsts.YearMinLength, HolidayConsts.YearMaxLength)]
        public int Year { get; set; }
        [StringLength(HolidayConsts.DescriptionMaxLength)]
        public string Description { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}