using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.UOMs
{
    public class UOMExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }

        public UOMExcelDownloadDto()
        {

        }
    }
}