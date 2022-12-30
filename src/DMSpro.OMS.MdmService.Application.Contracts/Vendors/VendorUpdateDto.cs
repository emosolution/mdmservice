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
        [Required]
        [StringLength(VendorConsts.ShortNameMaxLength, MinimumLength = VendorConsts.ShortNameMinLength)]
        public string ShortName { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string ERPCode { get; set; }
        public bool Active { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid WarehouseId { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Guid LinkedCompanyId { get; set; }
        public Guid PriceListId { get; set; }
        public Guid? GeoMaster0Id { get; set; }
        public Guid? GeoMaster1Id { get; set; }
        public Guid? GeoMaster2Id { get; set; }
        public Guid? GeoMaster3Id { get; set; }
        public Guid? GeoMaster4Id { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}