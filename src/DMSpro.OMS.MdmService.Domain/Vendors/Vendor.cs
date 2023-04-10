using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.Vendors
{
    public partial class Vendor : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        [CanBeNull]
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
        public Guid? LinkedCompanyId { get; set; }

        public Vendor()
        {

        }

        public Vendor(Guid id, Guid priceListId, Guid? geoMaster0Id, Guid? geoMaster1Id, Guid? geoMaster2Id, Guid? geoMaster3Id, Guid? geoMaster4Id, Guid companyId, Guid? linkedCompanyId, string code, string name, string shortName, string phone1, string phone2, string erpCode, bool active, string street, string address, string latitude, string longitude, DateTime? endDate = null)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), VendorConsts.CodeMaxLength, VendorConsts.CodeMinLength);
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), VendorConsts.NameMaxLength, VendorConsts.NameMinLength);
            Check.Length(shortName, nameof(shortName), VendorConsts.ShortNameMaxLength, 0);
            Check.Length(phone1, nameof(phone1), VendorConsts.Phone1MaxLength, 0);
            Check.Length(phone2, nameof(phone2), VendorConsts.Phone2MaxLength, 0);
            Check.Length(erpCode, nameof(erpCode), VendorConsts.ERPCodeMaxLength, 0);
            Check.Length(street, nameof(street), VendorConsts.StreetMaxLength, 0);
            Check.Length(address, nameof(address), VendorConsts.AddressMaxLength, 0);
            Check.Length(latitude, nameof(latitude), VendorConsts.LatitudeMaxLength, 0);
            Check.Length(longitude, nameof(longitude), VendorConsts.LongitudeMaxLength, 0);
            Code = code;
            Name = name;
            ShortName = shortName;
            Phone1 = phone1;
            Phone2 = phone2;
            ERPCode = erpCode;
            Active = active;
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
            LinkedCompanyId = linkedCompanyId;
        }

    }
}