using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.VATs
{
    public class VATExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public uint? RateMin { get; set; }
        public uint? RateMax { get; set; }

        public VATExcelDownloadDto()
        {

        }
    }
}