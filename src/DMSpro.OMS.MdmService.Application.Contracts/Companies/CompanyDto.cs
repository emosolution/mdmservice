using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.Companies
{
    public class CompanyDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string License { get; set; }
        public string TaxCode { get; set; }
        public string VATName { get; set; }
        public string VATAddress { get; set; }
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