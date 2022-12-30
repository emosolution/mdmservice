using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
    public class HolidayDetailExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public DateTime? StartDateMin { get; set; }
        public DateTime? StartDateMax { get; set; }
        public DateTime? EndDateMin { get; set; }
        public DateTime? EndDateMax { get; set; }
        public string Description { get; set; }
        public Guid? HolidayId { get; set; }

        public HolidayDetailExcelDownloadDto()
        {

        }
    }
}