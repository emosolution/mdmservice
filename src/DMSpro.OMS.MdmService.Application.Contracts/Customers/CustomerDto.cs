using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.Customers
{
    public class CustomerDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
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
        public DateTime? LastOrderDate { get; set; }
        public Guid? PaymentTermId { get; set; }
        public Guid? LinkedCompanyId { get; set; }
        public Guid? PriceListId { get; set; }
        public Guid? GeoMaster0Id { get; set; }
        public Guid? GeoMaster1Id { get; set; }
        public Guid? GeoMaster2Id { get; set; }
        public Guid? GeoMaster3Id { get; set; }
        public Guid? GeoMaster4Id { get; set; }
        public string FullAddress { get; set; }
        public Guid? Attr0Id { get; set; }
        public Guid? Attr1Id { get; set; }
        public Guid? Attr2Id { get; set; }
        public Guid? Attr3Id { get; set; }
        public Guid? Attr4Id { get; set; }
        public Guid? Attr5Id { get; set; }
        public Guid? Attr6Id { get; set; }
        public Guid? Attr7Id { get; set; }
        public Guid? Attr8Id { get; set; }
        public Guid? Attr9Id { get; set; }
        public Guid? Attr10Id { get; set; }
        public Guid? Attr11Id { get; set; }
        public Guid? Attr12Id { get; set; }
        public Guid? Attr13Id { get; set; }
        public Guid? Attr14Id { get; set; }
        public Guid? Attr15Id { get; set; }
        public Guid? Attr16Id { get; set; }
        public Guid? Attr17Id { get; set; }
        public Guid? Attr18Id { get; set; }
        public Guid? Attr19Id { get; set; }
        public Guid? PaymentId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}