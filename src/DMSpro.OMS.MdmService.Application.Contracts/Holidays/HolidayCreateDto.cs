using System.ComponentModel.DataAnnotations;

namespace DMSpro.OMS.MdmService.Holidays
{
    public class HolidayCreateDto
    {
        [Required]
        [Range(HolidayConsts.YearMinLength, HolidayConsts.YearMaxLength)]
        public int Year { get; set; }
        [StringLength(HolidayConsts.DescriptionMaxLength)]
        public string Description { get; set; }
    }
}