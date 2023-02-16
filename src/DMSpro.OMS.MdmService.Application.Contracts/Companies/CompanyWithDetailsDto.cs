using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.GeoMasters;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
namespace DMSpro.OMS.MdmService.Companies
{
    public class CompanyWithDetailsDto
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
        public DateTime? EndDate { get; set; }
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

        public string ParentCode{get;set;}
        public string ParentName{get;set;}
        public string GeoLevel0Name{get;set;}
        public string GeoLevel1Name{get;set;}
        public string GeoLevel2Name{get;set;}
        public string GeoLevel3Name{get;set;}
        public string GeoLevel4Name{get;set;}

        
        // public CompanyDto Company { get; set; }

        // public CompanyDto Company1 { get; set; }
        // public GeoMasterDto GeoMaster { get; set; }
        // public GeoMasterDto GeoMaster1 { get; set; }
        // public GeoMasterDto GeoMaster2 { get; set; }
        // public GeoMasterDto GeoMaster3 { get; set; }
        // public GeoMasterDto GeoMaster4 { get; set; }
    }
}