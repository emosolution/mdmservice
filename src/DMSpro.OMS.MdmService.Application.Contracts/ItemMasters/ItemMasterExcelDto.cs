using DMSpro.OMS.MdmService.ItemMasters;
using System;

namespace DMSpro.OMS.MdmService.ItemMasters
{
    public class ItemMasterExcelDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string ERPCode { get; set; }
        public string Barcode { get; set; }
        public bool Purchasble { get; set; }
        public bool Saleable { get; set; }
        public bool Inventoriable { get; set; }
        public bool Active { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public ManageType ManageType { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public ExpiredType ExpiredType { get; set; }
        public int ExpiredValue { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public IssueMethod IssueMethod { get; set; }
        public bool CanUpdate { get; set; }
        public int BasePrice { get; set; }
    }
}