using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.DimensionMeasurements
{
    public class DimensionMeasurementExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public uint? ValueMin { get; set; }
        public uint? ValueMax { get; set; }

        public DimensionMeasurementExcelDownloadDto()
        {

        }
    }
}