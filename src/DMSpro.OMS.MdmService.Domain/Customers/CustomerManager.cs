using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
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
        Guid? paymentTermId, Guid? linkedCompanyId, Guid? priceListId, Guid? geoMaster0Id, Guid? geoMaster1Id, Guid? geoMaster2Id, Guid? geoMaster3Id, Guid? geoMaster4Id, Guid? attr0Id, Guid? attr1Id, Guid? attr2Id, Guid? attr3Id, Guid? attr4Id, Guid? attr5Id, Guid? attr6Id, Guid? attr7Id, Guid? attr8Id, Guid? attr9Id, Guid? attr10Id, Guid? attr11Id, Guid? attr12Id, Guid? attr13Id, Guid? attr14Id, Guid? attr15Id, Guid? attr16Id, Guid? attr17Id, Guid? attr18Id, Guid? attr19Id, Guid? paymentId, string code, string name, string phone1, string phone2, string erpCode, string license, string taxCode, string vatName, string vatAddress, bool active, DateTime effectiveDate, bool isCompany, Guid warehouseId, string street, string address, string latitude, string longitude, string sfaCustomerCode, DateTime lastOrderDate, DateTime? endDate = null, int? creditLimit = null)
        {
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
             paymentTermId, linkedCompanyId, priceListId, geoMaster0Id, geoMaster1Id, geoMaster2Id, geoMaster3Id, geoMaster4Id, attr0Id, attr1Id, attr2Id, attr3Id, attr4Id, attr5Id, attr6Id, attr7Id, attr8Id, attr9Id, attr10Id, attr11Id, attr12Id, attr13Id, attr14Id, attr15Id, attr16Id, attr17Id, attr18Id, attr19Id, paymentId, code, name, phone1, phone2, erpCode, license, taxCode, vatName, vatAddress, active, effectiveDate, isCompany, warehouseId, street, address, latitude, longitude, sfaCustomerCode, lastOrderDate, endDate, creditLimit
             );

            return await _customerRepository.InsertAsync(customer);
        }

        public async Task<Customer> UpdateAsync(
            Guid id,
            Guid? paymentTermId, Guid? linkedCompanyId, Guid? priceListId, Guid? geoMaster0Id, Guid? geoMaster1Id, Guid? geoMaster2Id, Guid? geoMaster3Id, Guid? geoMaster4Id, Guid? attr0Id, Guid? attr1Id, Guid? attr2Id, Guid? attr3Id, Guid? attr4Id, Guid? attr5Id, Guid? attr6Id, Guid? attr7Id, Guid? attr8Id, Guid? attr9Id, Guid? attr10Id, Guid? attr11Id, Guid? attr12Id, Guid? attr13Id, Guid? attr14Id, Guid? attr15Id, Guid? attr16Id, Guid? attr17Id, Guid? attr18Id, Guid? attr19Id, Guid? paymentId, string code, string name, string phone1, string phone2, string erpCode, string license, string taxCode, string vatName, string vatAddress, bool active, DateTime effectiveDate, bool isCompany, Guid warehouseId, string street, string address, string latitude, string longitude, string sfaCustomerCode, DateTime lastOrderDate, DateTime? endDate = null, int? creditLimit = null, [CanBeNull] string concurrencyStamp = null
        )
        {
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
            customer.Attr0Id = attr0Id;
            customer.Attr1Id = attr1Id;
            customer.Attr2Id = attr2Id;
            customer.Attr3Id = attr3Id;
            customer.Attr4Id = attr4Id;
            customer.Attr5Id = attr5Id;
            customer.Attr6Id = attr6Id;
            customer.Attr7Id = attr7Id;
            customer.Attr8Id = attr8Id;
            customer.Attr9Id = attr9Id;
            customer.Attr10Id = attr10Id;
            customer.Attr11Id = attr11Id;
            customer.Attr12Id = attr12Id;
            customer.Attr13Id = attr13Id;
            customer.Attr14Id = attr14Id;
            customer.Attr15Id = attr15Id;
            customer.Attr16Id = attr16Id;
            customer.Attr17Id = attr17Id;
            customer.Attr18Id = attr18Id;
            customer.Attr19Id = attr19Id;
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