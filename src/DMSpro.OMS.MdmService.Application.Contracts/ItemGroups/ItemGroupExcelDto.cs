using DMSpro.OMS.MdmService.ItemMasters;
using System;

namespace DMSpro.OMS.MdmService.ItemGroups
{
    public class ItemGroupExcelDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public GroupType Type { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public Status Status { get; set; }
    }
}