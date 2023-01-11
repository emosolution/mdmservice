using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.Companies;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.Vendors
{
    public class Vendor : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        [NotNull]
        public virtual string ShortName { get; set; }

        [CanBeNull]
        public virtual string Phone1 { get; set; }

        [CanBeNull]
        public virtual string Phone2 { get; set; }

        [CanBeNull]
        public virtual string ERPCode { get; set; }

        public virtual bool Active { get; set; }

        public virtual DateTime? EndDate { get; set; }

        [CanBeNull]
        public virtual string LinkedCompany { get; set; }

        public virtual Guid WarehouseId { get; set; }

        [CanBeNull]
        public virtual string Street { get; set; }

        [CanBeNull]
        public virtual string Address { get; set; }

        [CanBeNull]
        public virtual string Latitude { get; set; }

        [CanBeNull]
        public virtual string Longitude { get; set; }
        public Guid PriceListId { get; set; }
        public Guid? GeoMaster0Id { get; set; }
        public Guid? GeoMaster1Id { get; set; }
        public Guid? GeoMaster2Id { get; set; }
        public Guid? GeoMaster3Id { get; set; }
        public Guid? GeoMaster4Id { get; set; }
        public Guid CompanyId { get; set; }

        public Vendor()
        {

        }

        public Vendor(Guid id, Guid priceListId, Guid? geoMaster0Id, Guid? geoMaster1Id, Guid? geoMaster2Id, Guid? geoMaster3Id, Guid? geoMaster4Id, Guid companyId, string code, string name, string shortName, string phone1, string phone2, string erpCode, bool active, string linkedCompany, Guid warehouseId, string street, string address, string latitude, string longitude, DateTime? endDate = null)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), VendorConsts.CodeMaxLength, VendorConsts.CodeMinLength);
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), VendorConsts.NameMaxLength, VendorConsts.NameMinLength);
            Check.NotNull(shortName, nameof(shortName));
            Check.Length(shortName, nameof(shortName), VendorConsts.ShortNameMaxLength, VendorConsts.ShortNameMinLength);
            Check.Length(linkedCompany, nameof(linkedCompany), VendorConsts.LinkedCompanyMaxLength, 0);
            Code = code;
            Name = name;
            ShortName = shortName;
            Phone1 = phone1;
            Phone2 = phone2;
            ERPCode = erpCode;
            Active = active;
            LinkedCompany = linkedCompany;
            WarehouseId = warehouseId;
            Street = street;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
            EndDate = endDate;
            PriceListId = priceListId;
            GeoMaster0Id = geoMaster0Id;
            GeoMaster1Id = geoMaster1Id;
            GeoMaster2Id = geoMaster2Id;
            GeoMaster3Id = geoMaster3Id;
            GeoMaster4Id = geoMaster4Id;
            CompanyId = companyId;
        }

    }
}