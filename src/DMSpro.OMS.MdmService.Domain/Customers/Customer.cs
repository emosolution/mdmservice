using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace DMSpro.OMS.MdmService.Customers
{
    public partial class Customer : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Code { get; set; }

        [CanBeNull]
        public virtual string Name { get; set; }

        [CanBeNull]
        public virtual string Phone1 { get; set; }

        [CanBeNull]
        public virtual string Phone2 { get; set; }

        [CanBeNull]
        public virtual string erpCode { get; set; }

        [CanBeNull]
        public virtual string License { get; set; }

        [CanBeNull]
        public virtual string TaxCode { get; set; }

        [CanBeNull]
        public virtual string vatName { get; set; }

        [CanBeNull]
        public virtual string vatAddress { get; set; }

        public virtual bool Active { get; set; }

        public virtual DateTime EffectiveDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual int? CreditLimit { get; set; }

        public virtual bool IsCompany { get; set; }

        public virtual Guid WarehouseId { get; set; }

        [CanBeNull]
        public virtual string Street { get; set; }

        [CanBeNull]
        public virtual string Address { get; set; }

        [CanBeNull]
        public virtual string Latitude { get; set; }

        [CanBeNull]
        public virtual string Longitude { get; set; }

        [CanBeNull]
        public virtual string SFACustomerCode { get; set; }

        public virtual DateTime LastOrderDate { get; set; }
        public Guid? PaymentTermId { get; set; }
        public Guid? LinkedCompanyId { get; set; }
        public Guid? PriceListId { get; set; }
        public Guid? GeoMaster0Id { get; set; }
        public Guid? GeoMaster1Id { get; set; }
        public Guid? GeoMaster2Id { get; set; }
        public Guid? GeoMaster3Id { get; set; }
        public Guid? GeoMaster4Id { get; set; }
        public Guid? Attr0Id { get; set; }
        public Guid? Attr1Id { get; set; }
        public Guid? Attr2Id { get; set; }
        public Guid? Attr3Id { get; set; }
        public Guid? Attr4Id { get; set; }
        public Guid? Attr5Id { get; set; }
        public Guid? Attr6Id { get; set; }
        public Guid? Attr7Id { get; set; }
        public Guid? Attr8Id { get; set; }
        public Guid? Attr9Id { get; set; }
        public Guid? Attr10Id { get; set; }
        public Guid? Attr11Id { get; set; }
        public Guid? Attr12Id { get; set; }
        public Guid? Attr13Id { get; set; }
        public Guid? Attr14Id { get; set; }
        public Guid? Attr15Id { get; set; }
        public Guid? Attr16Id { get; set; }
        public Guid? Attr17Id { get; set; }
        public Guid? Attr18Id { get; set; }
        public Guid? Attr19Id { get; set; }
        public Guid? PaymentId { get; set; }

        public Customer()
        {

        }

        public Customer(Guid id, Guid? paymentTermId, Guid? linkedCompanyId, Guid? priceListId, Guid? geoMaster0Id, Guid? geoMaster1Id, Guid? geoMaster2Id, Guid? geoMaster3Id, Guid? geoMaster4Id, Guid? attr0Id, Guid? attr1Id, Guid? attr2Id, Guid? attr3Id, Guid? attr4Id, Guid? attr5Id, Guid? attr6Id, Guid? attr7Id, Guid? attr8Id, Guid? attr9Id, Guid? attr10Id, Guid? attr11Id, Guid? attr12Id, Guid? attr13Id, Guid? attr14Id, Guid? attr15Id, Guid? attr16Id, Guid? attr17Id, Guid? attr18Id, Guid? attr19Id, Guid? paymentId, string code, string name, string phone1, string phone2, string erpCode, string license, string taxCode, string vatName, string vatAddress, bool active, DateTime effectiveDate, bool isCompany, Guid warehouseId, string street, string address, string latitude, string longitude, string sfaCustomerCode, DateTime lastOrderDate, DateTime? endDate = null, int? creditLimit = null)
        {

            Id = id;
            Check.NotNull(code, nameof(code));
            Check.Length(code, nameof(code), CustomerConsts.CodeMaxLength, CustomerConsts.CodeMinLength);
            Check.Length(name, nameof(name), CustomerConsts.NameMaxLength, 0);
            Check.Length(phone1, nameof(phone1), CustomerConsts.Phone1MaxLength, 0);
            Check.Length(phone2, nameof(phone2), CustomerConsts.Phone2MaxLength, 0);
            Check.Length(erpCode, nameof(erpCode), CustomerConsts.erpCodeMaxLength, 0);
            Check.Length(license, nameof(license), CustomerConsts.LicenseMaxLength, 0);
            Check.Length(taxCode, nameof(taxCode), CustomerConsts.TaxCodeMaxLength, 0);
            Check.Length(vatName, nameof(vatName), CustomerConsts.vatNameMaxLength, 0);
            Check.Length(vatAddress, nameof(vatAddress), CustomerConsts.vatAddressMaxLength, 0);
            Check.Length(street, nameof(street), CustomerConsts.StreetMaxLength, 0);
            Check.Length(address, nameof(address), CustomerConsts.AddressMaxLength, 0);
            Check.Length(latitude, nameof(latitude), CustomerConsts.LatitudeMaxLength, 0);
            Check.Length(longitude, nameof(longitude), CustomerConsts.LongitudeMaxLength, 0);
            Check.Length(sfaCustomerCode, nameof(sfaCustomerCode), CustomerConsts.SFACustomerCodeMaxLength, 0);
            Code = code;
            Name = name;
            Phone1 = phone1;
            Phone2 = phone2;
            this.erpCode = erpCode;
            License = license;
            TaxCode = taxCode;
            this.vatName = vatName;
            this.vatAddress = vatAddress;
            Active = active;
            EffectiveDate = effectiveDate;
            IsCompany = isCompany;
            WarehouseId = warehouseId;
            Street = street;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
            SFACustomerCode = sfaCustomerCode;
            LastOrderDate = lastOrderDate;
            EndDate = endDate;
            CreditLimit = creditLimit;
            PaymentTermId = paymentTermId;
            LinkedCompanyId = linkedCompanyId;
            PriceListId = priceListId;
            GeoMaster0Id = geoMaster0Id;
            GeoMaster1Id = geoMaster1Id;
            GeoMaster2Id = geoMaster2Id;
            GeoMaster3Id = geoMaster3Id;
            GeoMaster4Id = geoMaster4Id;
            Attr0Id = attr0Id;
            Attr1Id = attr1Id;
            Attr2Id = attr2Id;
            Attr3Id = attr3Id;
            Attr4Id = attr4Id;
            Attr5Id = attr5Id;
            Attr6Id = attr6Id;
            Attr7Id = attr7Id;
            Attr8Id = attr8Id;
            Attr9Id = attr9Id;
            Attr10Id = attr10Id;
            Attr11Id = attr11Id;
            Attr12Id = attr12Id;
            Attr13Id = attr13Id;
            Attr14Id = attr14Id;
            Attr15Id = attr15Id;
            Attr16Id = attr16Id;
            Attr17Id = attr17Id;
            Attr18Id = attr18Id;
            Attr19Id = attr19Id;
            PaymentId = paymentId;
        }

    }
}