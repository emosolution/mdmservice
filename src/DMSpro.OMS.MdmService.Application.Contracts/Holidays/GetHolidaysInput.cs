using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.Holidays
{
    public class GetHolidaysInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public int? YearMin { get; set; }
        public int? YearMax { get; set; }
        public string Description { get; set; }

        public GetHolidaysInput()
        {

        }
    }
}