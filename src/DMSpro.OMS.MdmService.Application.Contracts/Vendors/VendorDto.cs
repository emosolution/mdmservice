using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.Vendors
{
    public class VendorDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
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