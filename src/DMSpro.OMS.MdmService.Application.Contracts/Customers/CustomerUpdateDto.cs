using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.Customers
{
    public class CustomerUpdateDto : IHasConcurrencyStamp
    {
        [StringLength(CustomerConsts.NameMaxLength)]
        public string Name { get; set; }
        [StringLength(CustomerConsts.Phone1MaxLength)]
        public string Phone1 { get; set; }
        [StringLength(CustomerConsts.Phone2MaxLength)]
        public string Phone2 { get; set; }
        [StringLength(CustomerConsts.erpCodeMaxLength)]
        public string erpCode { get; set; }
        [StringLength(CustomerConsts.LicenseMaxLength)]
        public string License { get; set; }
        [StringLength(CustomerConsts.TaxCodeMaxLength)]
        public string TaxCode { get; set; }
        [StringLength(CustomerConsts.vatNameMaxLength)]
        public string vatName { get; set; }
        [StringLength(CustomerConsts.vatAddressMaxLength)]
        public string vatAddress { get; set; }
        public bool Active { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CreditLimit { get; set; }
        public bool IsCompany { get; set; }
        public Guid WarehouseId { get; set; }
        [StringLength(CustomerConsts.StreetMaxLength)]
        public string Street { get; set; }
        [StringLength(CustomerConsts.AddressMaxLength)]
        public string Address { get; set; }
        [StringLength(CustomerConsts.LatitudeMaxLength)]
        public string Latitude { get; set; }
        [StringLength(CustomerConsts.LongitudeMaxLength)]
        public string Longitude { get; set; }
        [StringLength(CustomerConsts.SFACustomerCodeMaxLength)]
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