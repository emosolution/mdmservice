using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.Vendors
{
    public class VendorUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(VendorConsts.CodeMaxLength, MinimumLength = VendorConsts.CodeMinLength)]
        public string Code { get; set; }
        [Required]
        [StringLength(VendorConsts.NameMaxLength, MinimumLength = VendorConsts.NameMinLength)]
        public string Name { get; set; }
        [StringLength(VendorConsts.ShortNameMaxLength)]
        public string ShortName { get; set; }
        [StringLength(VendorConsts.Phone1MaxLength)]
        public string Phone1 { get; set; }
        [StringLength(VendorConsts.Phone2MaxLength)]
        public string Phone2 { get; set; }
        [StringLength(VendorConsts.ERPCodeMaxLength)]
        public string ERPCode { get; set; }
        public bool Active { get; set; }
        public DateTime? EndDate { get; set; }
        [StringLength(VendorConsts.StreetMaxLength)]
        public string Street { get; set; }
        [StringLength(VendorConsts.AddressMaxLength)]
        public string Address { get; set; }
        [StringLength(VendorConsts.LatitudeMaxLength)]
        public string Latitude { get; set; }
        [StringLength(VendorConsts.LongitudeMaxLength)]
        public string Longitude { get; set; }
        public Guid PriceListId { get; set; }
        public Guid? GeoMaster0Id { get; set; }
        public Guid? GeoMaster1Id { get; set; }
        public Guid? GeoMaster2Id { get; set; }
        public Guid? GeoMaster3Id { get; set; }
        public Guid? GeoMaster4Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid? LinkedCompanyId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}