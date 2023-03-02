using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.Vendors
{
    public class VendorManager : DomainService
    {
        private readonly IVendorRepository _vendorRepository;

        public VendorManager(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        public async Task<Vendor> CreateAsync(
            Guid priceListId, Guid? geoMaster0Id, Guid? geoMaster1Id, Guid? geoMaster2Id, Guid? geoMaster3Id, Guid? geoMaster4Id, Guid companyId, Guid? linkedCompanyId, string code, string name, string shortName, string phone1, string phone2, string erpCode, bool active, string street, string address, string latitude, string longitude, DateTime? endDate = null)
        {
            Check.NotNull(priceListId, nameof(priceListId));
            Check.NotNull(companyId, nameof(companyId));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), VendorConsts.CodeMaxLength, VendorConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), VendorConsts.NameMaxLength, VendorConsts.NameMinLength);
            Check.Length(shortName, nameof(shortName), VendorConsts.ShortNameMaxLength);
            Check.Length(phone1, nameof(phone1), VendorConsts.Phone1MaxLength);
            Check.Length(phone2, nameof(phone2), VendorConsts.Phone2MaxLength);
            Check.Length(erpCode, nameof(erpCode), VendorConsts.ERPCodeMaxLength);
            Check.Length(street, nameof(street), VendorConsts.StreetMaxLength);
            Check.Length(address, nameof(address), VendorConsts.AddressMaxLength);
            Check.Length(latitude, nameof(latitude), VendorConsts.LatitudeMaxLength);
            Check.Length(longitude, nameof(longitude), VendorConsts.LongitudeMaxLength);

            var vendor = new Vendor(
             GuidGenerator.Create(),
             priceListId, geoMaster0Id, geoMaster1Id, geoMaster2Id, geoMaster3Id, geoMaster4Id, companyId, linkedCompanyId, code, name, shortName, phone1, phone2, erpCode, active, street, address, latitude, longitude, endDate
             );

            return await _vendorRepository.InsertAsync(vendor);
        }

        public async Task<Vendor> UpdateAsync(
            Guid id,
            Guid priceListId, Guid? geoMaster0Id, Guid? geoMaster1Id, Guid? geoMaster2Id, Guid? geoMaster3Id, Guid? geoMaster4Id, Guid companyId, Guid? linkedCompanyId, string code, string name, string shortName, string phone1, string phone2, string erpCode, bool active, string street, string address, string latitude, string longitude, DateTime? endDate = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(priceListId, nameof(priceListId));
            Check.NotNull(companyId, nameof(companyId));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), VendorConsts.CodeMaxLength, VendorConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), VendorConsts.NameMaxLength, VendorConsts.NameMinLength);
            Check.Length(shortName, nameof(shortName), VendorConsts.ShortNameMaxLength);
            Check.Length(phone1, nameof(phone1), VendorConsts.Phone1MaxLength);
            Check.Length(phone2, nameof(phone2), VendorConsts.Phone2MaxLength);
            Check.Length(erpCode, nameof(erpCode), VendorConsts.ERPCodeMaxLength);
            Check.Length(street, nameof(street), VendorConsts.StreetMaxLength);
            Check.Length(address, nameof(address), VendorConsts.AddressMaxLength);
            Check.Length(latitude, nameof(latitude), VendorConsts.LatitudeMaxLength);
            Check.Length(longitude, nameof(longitude), VendorConsts.LongitudeMaxLength);

            var vendor = await _vendorRepository.GetAsync(id);

            vendor.PriceListId = priceListId;
            vendor.GeoMaster0Id = geoMaster0Id;
            vendor.GeoMaster1Id = geoMaster1Id;
            vendor.GeoMaster2Id = geoMaster2Id;
            vendor.GeoMaster3Id = geoMaster3Id;
            vendor.GeoMaster4Id = geoMaster4Id;
            vendor.CompanyId = companyId;
            vendor.LinkedCompanyId = linkedCompanyId;
            vendor.Code = code;
            vendor.Name = name;
            vendor.ShortName = shortName;
            vendor.Phone1 = phone1;
            vendor.Phone2 = phone2;
            vendor.ERPCode = erpCode;
            vendor.Active = active;
            vendor.Street = street;
            vendor.Address = address;
            vendor.Latitude = latitude;
            vendor.Longitude = longitude;
            vendor.EndDate = endDate;

            vendor.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _vendorRepository.UpdateAsync(vendor);
        }

    }
}