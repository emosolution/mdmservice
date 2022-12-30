using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.Customers
{
    public class GetCustomersInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string erpCode { get; set; }
        public string License { get; set; }
        public string TaxCode { get; set; }
        public string vatName { get; set; }
        public string vatAddress { get; set; }
        public bool? Active { get; set; }
        public DateTime? EffectiveDateMin { get; set; }
        public DateTime? EffectiveDateMax { get; set; }
        public DateTime? EndDateMin { get; set; }
        public DateTime? EndDateMax { get; set; }
        public int? CreditLimitMin { get; set; }
        public int? CreditLimitMax { get; set; }
        public bool? IsCompany { get; set; }
        public Guid? WarehouseId { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string SFACustomerCode { get; set; }
        public DateTime? LastOrderDateMin { get; set; }
        public DateTime? LastOrderDateMax { get; set; }
        public Guid? PaymentTermId { get; set; }
        public Guid? LinkedCompanyId { get; set; }
        public Guid? PriceListId { get; set; }
        public Guid? GeoMaster0Id { get; set; }
        public Guid? GeoMaster1Id { get; set; }
        public Guid? GeoMaster2Id { get; set; }
        public Guid? GeoMaster3Id { get; set; }
        public Guid? GeoMaster4Id { get; set; }
        public Guid? Attribute0Id { get; set; }
        public Guid? Attribute1Id { get; set; }
        public Guid? Attribute2Id { get; set; }
        public Guid? Attribute3Id { get; set; }
        public Guid? Attribute4Id { get; set; }
        public Guid? Attribute5Id { get; set; }
        public Guid? Attribute6Id { get; set; }
        public Guid? Attribute7Id { get; set; }
        public Guid? Attribute8Id { get; set; }
        public Guid? Attribute9Id { get; set; }
        public Guid? Attribute10Id { get; set; }
        public Guid? Attribute11Id { get; set; }
        public Guid? Attribute12Id { get; set; }
        public Guid? Attribute13Id { get; set; }
        public Guid? Attribute14Id { get; set; }
        public Guid? Attribute15Id { get; set; }
        public Guid? Attribute16Id { get; set; }
        public Guid? Attribute1I7d { get; set; }
        public Guid? Attribute18Id { get; set; }
        public Guid? Attribute19Id { get; set; }
        public Guid? PaymentId { get; set; }

        public GetCustomersInput()
        {

        }
    }
}