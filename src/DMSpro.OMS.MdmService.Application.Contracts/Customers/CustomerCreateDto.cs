using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Customers
{
    public class CustomerCreateDto
    {
        [Required]
        [StringLength(CustomerConsts.CodeMaxLength, MinimumLength = CustomerConsts.CodeMinLength)]
        public string Code { get; set; }
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
        public bool Active { get; set; } = true;
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CreditLimit { get; set; }
        public bool IsCompany { get; set; } = false;
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
        public DateTime LastOrderDate { get; set; }
        public Guid? PaymentTermId { get; set; }
        public Guid? LinkedCompanyId { get; set; }
        public Guid PriceListId { get; set; }
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
        public Guid? Attribute17Id { get; set; }
        public Guid? Attribute18Id { get; set; }
        public Guid? Attribute19Id { get; set; }
        public Guid? PaymentId { get; set; }
    }
}