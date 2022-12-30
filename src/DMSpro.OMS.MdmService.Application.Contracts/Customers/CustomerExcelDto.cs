using System;

namespace DMSpro.OMS.MdmService.Customers
{
    public class CustomerExcelDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string erpCode { get; set; }
        public string License { get; set; }
        public string TaxCode { get; set; }
        public string vatName { get; set; }
        public string vatAddress { get; set; }
        public bool Active { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CreditLimit { get; set; }
        public bool IsCompany { get; set; }
        public Guid WarehouseId { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string SFACustomerCode { get; set; }
        public DateTime LastOrderDate { get; set; }
    }
}