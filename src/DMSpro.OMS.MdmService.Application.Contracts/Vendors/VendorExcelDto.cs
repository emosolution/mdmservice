using System;

namespace DMSpro.OMS.MdmService.Vendors
{
    public class VendorExcelDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string ERPCode { get; set; }
        public bool Active { get; set; }
        public DateTime? EndDate { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}