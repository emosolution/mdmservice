using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Companies
{
    public class CompanyCreateDto
    {
        [Required]
        [StringLength(CompanyConsts.CodeMaxLength, MinimumLength = CompanyConsts.CodeMinLength)]
        public string Code { get; set; }
        [Required]
        [StringLength(CompanyConsts.NameMaxLength, MinimumLength = CompanyConsts.NameMinLength)]
        public string Name { get; set; }
        [StringLength(CompanyConsts.StreetMaxLength)]
        public string Street { get; set; }
        [StringLength(CompanyConsts.AddressMaxLength)]
        public string Address { get; set; }
        [StringLength(CompanyConsts.PhoneMaxLength)]
        public string Phone { get; set; }
        [StringLength(CompanyConsts.LicenseMaxLength)]
        public string License { get; set; }
        [StringLength(CompanyConsts.TaxCodeMaxLength)]
        public string TaxCode { get; set; }
        [StringLength(CompanyConsts.VATNameMaxLength)]
        public string VATName { get; set; }
        [StringLength(CompanyConsts.VATAddressMaxLength)]
        public string VATAddress { get; set; }
        [StringLength(CompanyConsts.ERPCodeMaxLength)]
        public string ERPCode { get; set; }
        public bool Active { get; set; } = true;
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsHO { get; set; } = false;
        [StringLength(CompanyConsts.LatitudeMaxLength)]
        public string Latitude { get; set; }
        [StringLength(CompanyConsts.LongitudeMaxLength)]
        public string Longitude { get; set; }
        [StringLength(CompanyConsts.ContactNameMaxLength)]
        public string ContactName { get; set; }
        [StringLength(CompanyConsts.ContactPhoneMaxLength)]
        public string ContactPhone { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? GeoLevel0Id { get; set; }
        public Guid? GeoLevel1Id { get; set; }
        public Guid? GeoLevel2Id { get; set; }
        public Guid? GeoLevel3Id { get; set; }
        public Guid? GeoLevel4Id { get; set; }
    }
}