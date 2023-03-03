using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.CusAttributeValues;
using DMSpro.OMS.MdmService.Customers;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
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
        public Guid PriceListId { get; set; }
        public Guid? GeoMaster0Id { get; set; }
        public Guid? GeoMaster1Id { get; set; }
        public Guid? GeoMaster2Id { get; set; }
        public Guid? GeoMaster3Id { get; set; }
        public Guid? GeoMaster4Id { get; set; }
        public Guid? Attribute0Id { get; set; }
        public Guid? Attribute1Id { get; set; }
        public Guid? Attribute2Id { get; set; }
        public Guid? Attribute3Id { get; set; }
        public Guid? Attribute4Id { get; set; }
        public Guid? Attribute5Id { get; set; }
        public Guid? Attribute6Id { get; set; }
        public Guid? Attribute7Id { get; set; }
        public Guid? Attribute8Id { get; set; }
        public Guid? Attribute9Id { get; set; }
        public Guid? Attribute10Id { get; set; }
        public Guid? Attribute11Id { get; set; }
        public Guid? Attribute12Id { get; set; }
        public Guid? Attribute13Id { get; set; }
        public Guid? Attribute14Id { get; set; }
        public Guid? Attribute15Id { get; set; }
        public Guid? Attribute16Id { get; set; }
        public Guid? Attribute17Id { get; set; }
        public Guid? Attribute18Id { get; set; }
        public Guid? Attribute19Id { get; set; }
        public Guid? PaymentId { get; set; }

        public virtual GeoMaster GeoMaster0 { get; set; }
        public virtual GeoMaster GeoMaster1 { get; set; }
        public virtual GeoMaster GeoMaster2 { get; set; }
        public virtual GeoMaster GeoMaster3 { get; set; }
        public virtual GeoMaster GeoMaster4 { get; set; }
        public virtual PriceList PriceList { get; set; }
        //public virtual Company LinkedCompany { get; set; }
        public Customer()
        {

        }

        public Customer(Guid id, Guid? paymentTermId, Guid? linkedCompanyId, Guid priceListId, Guid? geoMaster0Id, Guid? geoMaster1Id, Guid? geoMaster2Id, Guid? geoMaster3Id, Guid? geoMaster4Id, Guid? attribute0Id, Guid? attribute1Id, Guid? attribute2Id, Guid? attribute3Id, Guid? attribute4Id, Guid? attribute5Id, Guid? attribute6Id, Guid? attribute7Id, Guid? attribute8Id, Guid? attribute9Id, Guid? attribute10Id, Guid? attribute11Id, Guid? attribute12Id, Guid? attribute13Id, Guid? attribute14Id, Guid? attribute15Id, Guid? attribute16Id, Guid? attribute17Id, Guid? attribute18Id, Guid? attribute19Id, Guid? paymentId, string code, string name, string phone1, string phone2, string erpCode, string license, string taxCode, string vatName, string vatAddress, bool active, DateTime effectiveDate, bool isCompany, Guid warehouseId, string street, string address, string latitude, string longitude, string sfaCustomerCode, DateTime lastOrderDate, DateTime? endDate = null, int? creditLimit = null)
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
            Attribute0Id = attribute0Id;
            Attribute1Id = attribute1Id;
            Attribute2Id = attribute2Id;
            Attribute3Id = attribute3Id;
            Attribute4Id = attribute4Id;
            Attribute5Id = attribute5Id;
            Attribute6Id = attribute6Id;
            Attribute7Id = attribute7Id;
            Attribute8Id = attribute8Id;
            Attribute9Id = attribute9Id;
            Attribute10Id = attribute10Id;
            Attribute11Id = attribute11Id;
            Attribute12Id = attribute12Id;
            Attribute13Id = attribute13Id;
            Attribute14Id = attribute14Id;
            Attribute15Id = attribute15Id;
            Attribute16Id = attribute16Id;
            Attribute17Id = attribute17Id;
            Attribute18Id = attribute18Id;
            Attribute19Id = attribute19Id;
            PaymentId = paymentId;
        }

    }
}