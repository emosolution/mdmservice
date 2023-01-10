using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.Companies
{
    public class CompanyManager : DomainService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyManager(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Company> CreateAsync(
        Guid? parentId, Guid? geoLevel0Id, Guid? geoLevel1Id, Guid? geoLevel2Id, Guid? geoLevel3Id, Guid? geoLevel4Id, string code, string name, string street, string address, string phone, string license, string taxCode, string vatName, string vatAddress, string erpCode, bool active, DateTime effectiveDate, bool isHO, string latitude, string longitude, string contactName, string contactPhone, DateTime? endDate = null)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), CompanyConsts.CodeMaxLength, CompanyConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), CompanyConsts.NameMaxLength, CompanyConsts.NameMinLength);
            Check.NotNullOrWhiteSpace(street, nameof(street));
            Check.NotNullOrWhiteSpace(address, nameof(address));
            Check.Length(address, nameof(address), CompanyConsts.AddressMaxLength, CompanyConsts.AddressMinLength);
            Check.Length(phone, nameof(phone), CompanyConsts.PhoneMaxLength, CompanyConsts.PhoneMinLength);
            Check.Length(license, nameof(license), CompanyConsts.LicenseMaxLength, CompanyConsts.LicenseMinLength);
            Check.Length(taxCode, nameof(taxCode), CompanyConsts.TaxCodeMaxLength);
            Check.Length(erpCode, nameof(erpCode), CompanyConsts.ERPCodeMaxLength);
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var company = new Company(
             GuidGenerator.Create(),
             parentId, geoLevel0Id, geoLevel1Id, geoLevel2Id, geoLevel3Id, geoLevel4Id, code, name, street, address, phone, license, taxCode, vatName, vatAddress, erpCode, active, effectiveDate, isHO, latitude, longitude, contactName, contactPhone, endDate
             );

            return await _companyRepository.InsertAsync(company);
        }

        public async Task<Company> UpdateAsync(
            Guid id,
            Guid? parentId, Guid? geoLevel0Id, Guid? geoLevel1Id, Guid? geoLevel2Id, Guid? geoLevel3Id, Guid? geoLevel4Id, string code, string name, string street, string address, string phone, string license, string taxCode, string vatName, string vatAddress, string erpCode, bool active, DateTime effectiveDate, bool isHO, string latitude, string longitude, string contactName, string contactPhone, DateTime? endDate = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), CompanyConsts.CodeMaxLength, CompanyConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), CompanyConsts.NameMaxLength, CompanyConsts.NameMinLength);
            Check.NotNullOrWhiteSpace(street, nameof(street));
            Check.NotNullOrWhiteSpace(address, nameof(address));
            Check.Length(address, nameof(address), CompanyConsts.AddressMaxLength, CompanyConsts.AddressMinLength);
            Check.Length(phone, nameof(phone), CompanyConsts.PhoneMaxLength, CompanyConsts.PhoneMinLength);
            Check.Length(license, nameof(license), CompanyConsts.LicenseMaxLength, CompanyConsts.LicenseMinLength);
            Check.Length(taxCode, nameof(taxCode), CompanyConsts.TaxCodeMaxLength);
            Check.Length(erpCode, nameof(erpCode), CompanyConsts.ERPCodeMaxLength);
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var queryable = await _companyRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var company = await AsyncExecuter.FirstOrDefaultAsync(query);

            company.ParentId = parentId;
            company.GeoLevel0Id = geoLevel0Id;
            company.GeoLevel1Id = geoLevel1Id;
            company.GeoLevel2Id = geoLevel2Id;
            company.GeoLevel3Id = geoLevel3Id;
            company.GeoLevel4Id = geoLevel4Id;
            company.Code = code;
            company.Name = name;
            company.Street = street;
            company.Address = address;
            company.Phone = phone;
            company.License = license;
            company.TaxCode = taxCode;
            company.VATName = vatName;
            company.VATAddress = vatAddress;
            company.ERPCode = erpCode;
            company.Active = active;
            company.EffectiveDate = effectiveDate;
            company.EndDate = endDate;
            company.IsHO = isHO;
            company.Latitude = latitude;
            company.Longitude = longitude;
            company.ContactName = contactName;
            company.ContactPhone = contactPhone;

            company.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _companyRepository.UpdateAsync(company);
        }

    }
}