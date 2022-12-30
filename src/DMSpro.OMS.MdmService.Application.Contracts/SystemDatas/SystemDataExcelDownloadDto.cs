using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.SystemDatas
{
    public class SystemDataExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string Code { get; set; }
        public string ValueCode { get; set; }
        public string ValueName { get; set; }

        public SystemDataExcelDownloadDto()
        {

        }
    }
}