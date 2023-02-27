using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;
using DMSpro.OMS.MdmService.GeoMasters;

namespace DMSpro.OMS.MdmService.Companies
{
    public partial class Company : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        [CanBeNull]
        public virtual string Street { get; set; }

        [CanBeNull]
        public virtual string Address { get; set; }

        [CanBeNull]
        public virtual string Phone { get; set; }

        [CanBeNull]
        public virtual string License { get; set; }

        [CanBeNull]
        public virtual string TaxCode { get; set; }

        [CanBeNull]
        public virtual string VATName { get; set; }

        [CanBeNull]
        public virtual string VATAddress { get; set; }

        [CanBeNull]
        public virtual string ERPCode { get; set; }

        public virtual bool Active { get; set; }

        public virtual DateTime EffectiveDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual bool IsHO { get; set; }

        [CanBeNull]
        public virtual string Latitude { get; set; }

        [CanBeNull]
        public virtual string Longitude { get; set; }

        [CanBeNull]
        public virtual string ContactName { get; set; }

        [CanBeNull]
        public virtual string ContactPhone { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? GeoLevel0Id { get; set; }
        public Guid? GeoLevel1Id { get; set; }
        public Guid? GeoLevel2Id { get; set; }
        public Guid? GeoLevel3Id { get; set; }
        public Guid? GeoLevel4Id { get; set; }

        public virtual Company Parent { get; set; }
        public virtual GeoMaster GeoLevel0 { get; set; }
        public virtual GeoMaster GeoLevel1 { get; set; }
        public virtual GeoMaster GeoLevel2 { get; set; }
        public virtual GeoMaster GeoLevel3 { get; set; }
        public virtual GeoMaster GeoLevel4 { get; set; }
        public Company()
        {

        }

        public Company(Guid id, Guid? parentId, Guid? geoLevel0Id, Guid? geoLevel1Id, Guid? geoLevel2Id, Guid? geoLevel3Id, Guid? geoLevel4Id, string code, string name, string street, string address, string phone, string license, string taxCode, string vatName, string vatAddress, string erpCode, bool active, DateTime effectiveDate, bool isHO, string latitude, string longitude, string contactName, string contactPhone, DateTime? endDate = null)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), CompanyConsts.CodeMaxLength, CompanyConsts.CodeMinLength);
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), CompanyConsts.NameMaxLength, CompanyConsts.NameMinLength);
            Check.Length(street, nameof(street), CompanyConsts.StreetMaxLength, 0);
            Check.Length(address, nameof(address), CompanyConsts.AddressMaxLength, 0);
            Check.Length(phone, nameof(phone), CompanyConsts.PhoneMaxLength, 0);
            Check.Length(license, nameof(license), CompanyConsts.LicenseMaxLength, 0);
            Check.Length(taxCode, nameof(taxCode), CompanyConsts.TaxCodeMaxLength, 0);
            Check.Length(vatName, nameof(vatName), CompanyConsts.VATNameMaxLength, 0);
            Check.Length(vatAddress, nameof(vatAddress), CompanyConsts.VATAddressMaxLength, 0);
            Check.Length(erpCode, nameof(erpCode), CompanyConsts.ERPCodeMaxLength, 0);
            Check.Length(latitude, nameof(latitude), CompanyConsts.LatitudeMaxLength, 0);
            Check.Length(longitude, nameof(longitude), CompanyConsts.LongitudeMaxLength, 0);
            Check.Length(contactName, nameof(contactName), CompanyConsts.ContactNameMaxLength, 0);
            Check.Length(contactPhone, nameof(contactPhone), CompanyConsts.ContactPhoneMaxLength, 0);
            Code = code;
            Name = name;
            Street = street;
            Address = address;
            Phone = phone;
            License = license;
            TaxCode = taxCode;
            VATName = vatName;
            VATAddress = vatAddress;
            ERPCode = erpCode;
            Active = active;
            EffectiveDate = effectiveDate;
            EndDate = endDate;
            IsHO = isHO;
            Latitude = latitude;
            Longitude = longitude;
            ContactName = contactName;
            ContactPhone = contactPhone;
            ParentId = parentId;
            GeoLevel0Id = geoLevel0Id;
            GeoLevel1Id = geoLevel1Id;
            GeoLevel2Id = geoLevel2Id;
            GeoLevel3Id = geoLevel3Id;
            GeoLevel4Id = geoLevel4Id;
        }

    }
}