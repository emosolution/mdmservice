using DMSpro.OMS.MdmService.SystemConfigs;
using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.SystemConfigs
{
    public class SystemConfigExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string Code { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string DefaultValue { get; set; }
        public bool? EditableByTenant { get; set; }
        public ControlType? ControlType { get; set; }
        public string DataSource { get; set; }

        public SystemConfigExcelDownloadDto()
        {

        }
    }
}