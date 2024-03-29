﻿using System;
using DMSpro.OMS.MdmService.GeoMasters;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
namespace DMSpro.OMS.MdmService.Companies
{
	public class CompanyWithDetailsDto:  FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
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

        public string ConcurrencyStamp { get; set; }

        public CompanyDto Parent { get; set; }
		public GeoMasterDto GeoLevel0 { get; set; }
        public GeoMasterDto GeoLevel1 { get; set; }
        public GeoMasterDto GeoLevel2 { get; set; }
        public GeoMasterDto GeoLevel3 { get; set; }
        public GeoMasterDto GeoLevel4 { get; set; }

        public CompanyWithDetailsDto()
		{
		}
	}
}

