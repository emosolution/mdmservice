using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.WeightMeasurements
{
    public class WeightMeasurementExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public decimal? ValueMin { get; set; }
        public decimal? ValueMax { get; set; }

        public WeightMeasurementExcelDownloadDto()
        {

        }
    }
}