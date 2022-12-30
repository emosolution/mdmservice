using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.Streets
{
    public class StreetExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string Name { get; set; }

        public StreetExcelDownloadDto()
        {

        }
    }
}