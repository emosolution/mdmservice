using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.Customers
{
    public class CustomerManager : DomainService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> CreateAsync(
        Guid? paymentTermId, Guid? linkedCompanyId, Guid priceListId, Guid? geoMaster0Id, Guid? geoMaster1Id, Guid? geoMaster2Id, Guid? geoMaster3Id, Guid? geoMaster4Id, Guid? attribute0Id, Guid? attribute1Id, Guid? attribute2Id, Guid? attribute3Id, Guid? attribute4Id, Guid? attribute5Id, Guid? attribute6Id, Guid? attribute7Id, Guid? attribute8Id, Guid? attribute9Id, Guid? attribute10Id, Guid? attribute11Id, Guid? attribute12Id, Guid? attribute13Id, Guid? attribute14Id, Guid? attribute15Id, Guid? attribute16Id, Guid? attribute17Id, Guid? attribute18Id, Guid? attribute19Id, Guid? paymentId, string code, string name, string phone1, string phone2, string erpCode, string license, string taxCode, string vatName, string vatAddress, bool active, DateTime effectiveDate, bool isCompany, Guid warehouseId, string street, string address, string latitude, string longitude, string sfaCustomerCode, DateTime lastOrderDate, DateTime? endDate = null, int? creditLimit = null)
        {
            Check.NotNull(priceListId, nameof(priceListId));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), CustomerConsts.CodeMaxLength, CustomerConsts.CodeMinLength);
            Check.Length(name, nameof(name), CustomerConsts.NameMaxLength);
            Check.Length(phone1, nameof(phone1), CustomerConsts.Phone1MaxLength);
            Check.Length(phone2, nameof(phone2), CustomerConsts.Phone2MaxLength);
            Check.Length(erpCode, nameof(erpCode), CustomerConsts.erpCodeMaxLength);
            Check.Length(license, nameof(license), CustomerConsts.LicenseMaxLength);
            Check.Length(taxCode, nameof(taxCode), CustomerConsts.TaxCodeMaxLength);
            Check.Length(vatName, nameof(vatName), CustomerConsts.vatNameMaxLength);
            Check.Length(vatAddress, nameof(vatAddress), CustomerConsts.vatAddressMaxLength);
            Check.NotNull(effectiveDate, nameof(effectiveDate));
            Check.Length(street, nameof(street), CustomerConsts.StreetMaxLength);
            Check.Length(address, nameof(address), CustomerConsts.AddressMaxLength);
            Check.Length(latitude, nameof(latitude), CustomerConsts.LatitudeMaxLength);
            Check.Length(longitude, nameof(longitude), CustomerConsts.LongitudeMaxLength);
            Check.Length(sfaCustomerCode, nameof(sfaCustomerCode), CustomerConsts.SFACustomerCodeMaxLength);
            Check.NotNull(lastOrderDate, nameof(lastOrderDate));

            var customer = new Customer(
             GuidGenerator.Create(),
             paymentTermId, linkedCompanyId, priceListId, geoMaster0Id, geoMaster1Id, geoMaster2Id, geoMaster3Id, geoMaster4Id, attribute0Id, attribute1Id, attribute2Id, attribute3Id, attribute4Id, attribute5Id, attribute6Id, attribute7Id, attribute8Id, attribute9Id, attribute10Id, attribute11Id, attribute12Id, attribute13Id, attribute14Id, attribute15Id, attribute16Id, attribute17Id, attribute18Id, attribute19Id, paymentId, code, name, phone1, phone2, erpCode, license, taxCode, vatName, vatAddress, active, effectiveDate, isCompany, warehouseId, street, address, latitude, longitude, sfaCustomerCode, lastOrderDate, endDate, creditLimit
             );

            return await _customerRepository.InsertAsync(customer);
        }

        public async Task<Customer> UpdateAsync(
            Guid id,
            Guid? paymentTermId, Guid? linkedCompanyId, Guid priceListId, Guid? geoMaster0Id, Guid? geoMaster1Id, Guid? geoMaster2Id, Guid? geoMaster3Id, Guid? geoMaster4Id, Guid? attribute0Id, Guid? attribute1Id, Guid? attribute2Id, Guid? attribute3Id, Guid? attribute4Id, Guid? attribute5Id, Guid? attribute6Id, Guid? attribute7Id, Guid? attribute8Id, Guid? attribute9Id, Guid? attribute10Id, Guid? attribute11Id, Guid? attribute12Id, Guid? attribute13Id, Guid? attribute14Id, Guid? attribute15Id, Guid? attribute16Id, Guid? attribute17Id, Guid? attribute18Id, Guid? attribute19Id, Guid? paymentId, string code, string name, string phone1, string phone2, string erpCode, string license, string taxCode, string vatName, string vatAddress, bool active, DateTime effectiveDate, bool isCompany, Guid warehouseId, string street, string address, string latitude, string longitude, string sfaCustomerCode, DateTime lastOrderDate, DateTime? endDate = null, int? creditLimit = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(priceListId, nameof(priceListId));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), CustomerConsts.CodeMaxLength, CustomerConsts.CodeMinLength);
            Check.Length(name, nameof(name), CustomerConsts.NameMaxLength);
            Check.Length(phone1, nameof(phone1), CustomerConsts.Phone1MaxLength);
            Check.Length(phone2, nameof(phone2), CustomerConsts.Phone2MaxLength);
            Check.Length(erpCode, nameof(erpCode), CustomerConsts.erpCodeMaxLength);
            Check.Length(license, nameof(license), CustomerConsts.LicenseMaxLength);
            Check.Length(taxCode, nameof(taxCode), CustomerConsts.TaxCodeMaxLength);
            Check.Length(vatName, nameof(vatName), CustomerConsts.vatNameMaxLength);
            Check.Length(vatAddress, nameof(vatAddress), CustomerConsts.vatAddressMaxLength);
            Check.NotNull(effectiveDate, nameof(effectiveDate));
            Check.Length(street, nameof(street), CustomerConsts.StreetMaxLength);
            Check.Length(address, nameof(address), CustomerConsts.AddressMaxLength);
            Check.Length(latitude, nameof(latitude), CustomerConsts.LatitudeMaxLength);
            Check.Length(longitude, nameof(longitude), CustomerConsts.LongitudeMaxLength);
            Check.Length(sfaCustomerCode, nameof(sfaCustomerCode), CustomerConsts.SFACustomerCodeMaxLength);
            Check.NotNull(lastOrderDate, nameof(lastOrderDate));

            var customer = await _customerRepository.GetAsync(id);

            customer.PaymentTermId = paymentTermId;
            customer.LinkedCompanyId = linkedCompanyId;
            customer.PriceListId = priceListId;
            customer.GeoMaster0Id = geoMaster0Id;
            customer.GeoMaster1Id = geoMaster1Id;
            customer.GeoMaster2Id = geoMaster2Id;
            customer.GeoMaster3Id = geoMaster3Id;
            customer.GeoMaster4Id = geoMaster4Id;
            customer.Attribute0Id = attribute0Id;
            customer.Attribute1Id = attribute1Id;
            customer.Attribute2Id = attribute2Id;
            customer.Attribute3Id = attribute3Id;
            customer.Attribute4Id = attribute4Id;
            customer.Attribute5Id = attribute5Id;
            customer.Attribute6Id = attribute6Id;
            customer.Attribute7Id = attribute7Id;
            customer.Attribute8Id = attribute8Id;
            customer.Attribute9Id = attribute9Id;
            customer.Attribute10Id = attribute10Id;
            customer.Attribute11Id = attribute11Id;
            customer.Attribute12Id = attribute12Id;
            customer.Attribute13Id = attribute13Id;
            customer.Attribute14Id = attribute14Id;
            customer.Attribute15Id = attribute15Id;
            customer.Attribute16Id = attribute16Id;
            customer.Attribute17Id = attribute17Id;
            customer.Attribute18Id = attribute18Id;
            customer.Attribute19Id = attribute19Id;
            customer.PaymentId = paymentId;
            customer.Code = code;
            customer.Name = name;
            customer.Phone1 = phone1;
            customer.Phone2 = phone2;
            customer.erpCode = erpCode;
            customer.License = license;
            customer.TaxCode = taxCode;
            customer.vatName = vatName;
            customer.vatAddress = vatAddress;
            customer.Active = active;
            customer.EffectiveDate = effectiveDate;
            customer.IsCompany = isCompany;
            customer.WarehouseId = warehouseId;
            customer.Street = street;
            customer.Address = address;
            customer.Latitude = latitude;
            customer.Longitude = longitude;
            customer.SFACustomerCode = sfaCustomerCode;
            customer.LastOrderDate = lastOrderDate;
            customer.EndDate = endDate;
            customer.CreditLimit = creditLimit;

            customer.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerRepository.UpdateAsync(customer);
        }

    }
}