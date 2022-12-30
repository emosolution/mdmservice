using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.Companies
{
    public class CompanyUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(CompanyConsts.CodeMaxLength, MinimumLength = CompanyConsts.CodeMinLength)]
        public string Code { get; set; }
        [Required]
        [StringLength(CompanyConsts.NameMaxLength, MinimumLength = CompanyConsts.NameMinLength)]
        public string Name { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        [StringLength(CompanyConsts.AddressMaxLength, MinimumLength = CompanyConsts.AddressMinLength)]
        public string Address { get; set; }
        [StringLength(CompanyConsts.PhoneMaxLength, MinimumLength = CompanyConsts.PhoneMinLength)]
        public string Phone { get; set; }
        [StringLength(CompanyConsts.LicenseMaxLength, MinimumLength = CompanyConsts.LicenseMinLength)]
        public string License { get; set; }
        [StringLength(CompanyConsts.TaxCodeMaxLength)]
        public string TaxCode { get; set; }
        public string VATName { get; set; }
        public string VATAddress { get; set; }
        [StringLength(CompanyConsts.ERPCodeMaxLength)]
        public string ERPCode { get; set; }
        public bool Active { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsHO { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? GeoLevel0Id { get; set; }
        public Guid? GeoLevel1Id { get; set; }
        public Guid? GeoLevel2Id { get; set; }
        public Guid? GeoLevel3Id { get; set; }
        public Guid? GeoLevel4Id { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}