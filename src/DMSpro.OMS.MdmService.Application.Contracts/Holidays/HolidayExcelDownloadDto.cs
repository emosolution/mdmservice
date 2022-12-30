using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.Holidays
{
    public class HolidayExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public int? YearMin { get; set; }
        public int? YearMax { get; set; }
        public string Description { get; set; }

        public HolidayExcelDownloadDto()
        {

        }
    }
}