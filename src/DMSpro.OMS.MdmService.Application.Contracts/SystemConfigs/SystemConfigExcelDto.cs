using DMSpro.OMS.MdmService.SystemConfigs;
using System;

namespace DMSpro.OMS.MdmService.SystemConfigs
{
    public class SystemConfigExcelDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string DefaultValue { get; set; }
        public bool EditableByTenant { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public ControlType ControlType { get; set; }
        public string DataSource { get; set; }
    }
}