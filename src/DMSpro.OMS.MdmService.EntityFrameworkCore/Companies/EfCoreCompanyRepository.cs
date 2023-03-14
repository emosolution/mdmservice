using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using DMSpro.OMS.MdmService.CompanyIdentityUserAssignments;

namespace DMSpro.OMS.MdmService.Companies
{
    public partial class EfCoreCompanyRepository : EfCoreRepository<MdmServiceDbContext, Company, Guid>, ICompanyRepository
    {
        private readonly ICompanyIdentityUserAssignmentRepository _companyIdentityUserAssignmentRepository;

        public EfCoreCompanyRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider,
             ICompanyIdentityUserAssignmentRepository companyIdentityUserAssignmentRepository)
            : base(dbContextProvider)
        {
            _companyIdentityUserAssignmentRepository = companyIdentityUserAssignmentRepository;
        }

        public async Task<CompanyWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(company => new CompanyWithNavigationProperties
                {
                    Company = company,
                    Company1 = dbContext.Companies.FirstOrDefault(c => c.Id == company.ParentId),
                    GeoMaster = dbContext.GeoMasters.FirstOrDefault(c => c.Id == company.GeoLevel0Id),
                    GeoMaster1 = dbContext.GeoMasters.FirstOrDefault(c => c.Id == company.GeoLevel1Id),
                    GeoMaster2 = dbContext.GeoMasters.FirstOrDefault(c => c.Id == company.GeoLevel2Id),
                    GeoMaster3 = dbContext.GeoMasters.FirstOrDefault(c => c.Id == company.GeoLevel3Id),
                    GeoMaster4 = dbContext.GeoMasters.FirstOrDefault(c => c.Id == company.GeoLevel4Id)
                }).FirstOrDefault();
        }

        public async Task<List<CompanyWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string street = null,
            string address = null,
            string phone = null,
            string license = null,
            string taxCode = null,
            string vatName = null,
            string vatAddress = null,
            string erpCode = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            bool? isHO = null,
            string latitude = null,
            string longitude = null,
            string contactName = null,
            string contactPhone = null,
            Guid? parentId = null,
            Guid? geoLevel0Id = null,
            Guid? geoLevel1Id = null,
            Guid? geoLevel2Id = null,
            Guid? geoLevel3Id = null,
            Guid? geoLevel4Id = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, street, address, phone, license, taxCode, vatName, vatAddress, erpCode, active, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, isHO, latitude, longitude, contactName, contactPhone, parentId, geoLevel0Id, geoLevel1Id, geoLevel2Id, geoLevel3Id, geoLevel4Id);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CompanyConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CompanyWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from company in (await GetDbSetAsync())
                   join company1 in (await GetDbContextAsync()).Companies on company.ParentId equals company1.Id into companies1
                   from company1 in companies1.DefaultIfEmpty()
                   join geoMaster in (await GetDbContextAsync()).GeoMasters on company.GeoLevel0Id equals geoMaster.Id into geoMasters
                   from geoMaster in geoMasters.DefaultIfEmpty()
                   join geoMaster1 in (await GetDbContextAsync()).GeoMasters on company.GeoLevel1Id equals geoMaster1.Id into geoMasters1
                   from geoMaster1 in geoMasters1.DefaultIfEmpty()
                   join geoMaster2 in (await GetDbContextAsync()).GeoMasters on company.GeoLevel2Id equals geoMaster2.Id into geoMasters2
                   from geoMaster2 in geoMasters2.DefaultIfEmpty()
                   join geoMaster3 in (await GetDbContextAsync()).GeoMasters on company.GeoLevel3Id equals geoMaster3.Id into geoMasters3
                   from geoMaster3 in geoMasters3.DefaultIfEmpty()
                   join geoMaster4 in (await GetDbContextAsync()).GeoMasters on company.GeoLevel4Id equals geoMaster4.Id into geoMasters4
                   from geoMaster4 in geoMasters4.DefaultIfEmpty()

                   select new CompanyWithNavigationProperties
                   {
                       Company = company,
                       Company1 = company1,
                       GeoMaster = geoMaster,
                       GeoMaster1 = geoMaster1,
                       GeoMaster2 = geoMaster2,
                       GeoMaster3 = geoMaster3,
                       GeoMaster4 = geoMaster4
                   };
        }

        protected virtual IQueryable<CompanyWithNavigationProperties> ApplyFilter(
            IQueryable<CompanyWithNavigationProperties> query,
            string filterText,
            string code = null,
            string name = null,
            string street = null,
            string address = null,
            string phone = null,
            string license = null,
            string taxCode = null,
            string vatName = null,
            string vatAddress = null,
            string erpCode = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            bool? isHO = null,
            string latitude = null,
            string longitude = null,
            string contactName = null,
            string contactPhone = null,
            Guid? parentId = null,
            Guid? geoLevel0Id = null,
            Guid? geoLevel1Id = null,
            Guid? geoLevel2Id = null,
            Guid? geoLevel3Id = null,
            Guid? geoLevel4Id = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Company.Code.Contains(filterText) || e.Company.Name.Contains(filterText) || e.Company.Street.Contains(filterText) || e.Company.Address.Contains(filterText) || e.Company.Phone.Contains(filterText) || e.Company.License.Contains(filterText) || e.Company.TaxCode.Contains(filterText) || e.Company.VATName.Contains(filterText) || e.Company.VATAddress.Contains(filterText) || e.Company.ERPCode.Contains(filterText) || e.Company.Latitude.Contains(filterText) || e.Company.Longitude.Contains(filterText) || e.Company.ContactName.Contains(filterText) || e.Company.ContactPhone.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Company.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Company.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(street), e => e.Company.Street.Contains(street))
                    .WhereIf(!string.IsNullOrWhiteSpace(address), e => e.Company.Address.Contains(address))
                    .WhereIf(!string.IsNullOrWhiteSpace(phone), e => e.Company.Phone.Contains(phone))
                    .WhereIf(!string.IsNullOrWhiteSpace(license), e => e.Company.License.Contains(license))
                    .WhereIf(!string.IsNullOrWhiteSpace(taxCode), e => e.Company.TaxCode.Contains(taxCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(vatName), e => e.Company.VATName.Contains(vatName))
                    .WhereIf(!string.IsNullOrWhiteSpace(vatAddress), e => e.Company.VATAddress.Contains(vatAddress))
                    .WhereIf(!string.IsNullOrWhiteSpace(erpCode), e => e.Company.ERPCode.Contains(erpCode))
                    .WhereIf(active.HasValue, e => e.Company.Active == active)
                    .WhereIf(effectiveDateMin.HasValue, e => e.Company.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.Company.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.Company.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.Company.EndDate <= endDateMax.Value)
                    .WhereIf(isHO.HasValue, e => e.Company.IsHO == isHO)
                    .WhereIf(!string.IsNullOrWhiteSpace(latitude), e => e.Company.Latitude.Contains(latitude))
                    .WhereIf(!string.IsNullOrWhiteSpace(longitude), e => e.Company.Longitude.Contains(longitude))
                    .WhereIf(!string.IsNullOrWhiteSpace(contactName), e => e.Company.ContactName.Contains(contactName))
                    .WhereIf(!string.IsNullOrWhiteSpace(contactPhone), e => e.Company.ContactPhone.Contains(contactPhone))
                    .WhereIf(parentId != null && parentId != Guid.Empty, e => e.Company1 != null && e.Company1.Id == parentId)
                    .WhereIf(geoLevel0Id != null && geoLevel0Id != Guid.Empty, e => e.GeoMaster != null && e.GeoMaster.Id == geoLevel0Id)
                    .WhereIf(geoLevel1Id != null && geoLevel1Id != Guid.Empty, e => e.GeoMaster1 != null && e.GeoMaster1.Id == geoLevel1Id)
                    .WhereIf(geoLevel2Id != null && geoLevel2Id != Guid.Empty, e => e.GeoMaster2 != null && e.GeoMaster2.Id == geoLevel2Id)
                    .WhereIf(geoLevel3Id != null && geoLevel3Id != Guid.Empty, e => e.GeoMaster3 != null && e.GeoMaster3.Id == geoLevel3Id)
                    .WhereIf(geoLevel4Id != null && geoLevel4Id != Guid.Empty, e => e.GeoMaster4 != null && e.GeoMaster4.Id == geoLevel4Id);
        }

        public async Task<List<Company>> GetListAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string street = null,
            string address = null,
            string phone = null,
            string license = null,
            string taxCode = null,
            string vatName = null,
            string vatAddress = null,
            string erpCode = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            bool? isHO = null,
            string latitude = null,
            string longitude = null,
            string contactName = null,
            string contactPhone = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, name, street, address, phone, license, taxCode, vatName, vatAddress, erpCode, active, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, isHO, latitude, longitude, contactName, contactPhone);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CompanyConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string street = null,
            string address = null,
            string phone = null,
            string license = null,
            string taxCode = null,
            string vatName = null,
            string vatAddress = null,
            string erpCode = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            bool? isHO = null,
            string latitude = null,
            string longitude = null,
            string contactName = null,
            string contactPhone = null,
            Guid? parentId = null,
            Guid? geoLevel0Id = null,
            Guid? geoLevel1Id = null,
            Guid? geoLevel2Id = null,
            Guid? geoLevel3Id = null,
            Guid? geoLevel4Id = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, street, address, phone, license, taxCode, vatName, vatAddress, erpCode, active, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, isHO, latitude, longitude, contactName, contactPhone, parentId, geoLevel0Id, geoLevel1Id, geoLevel2Id, geoLevel3Id, geoLevel4Id);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Company> ApplyFilter(
            IQueryable<Company> query,
            string filterText,
            string code = null,
            string name = null,
            string street = null,
            string address = null,
            string phone = null,
            string license = null,
            string taxCode = null,
            string vatName = null,
            string vatAddress = null,
            string erpCode = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            bool? isHO = null,
            string latitude = null,
            string longitude = null,
            string contactName = null,
            string contactPhone = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.Name.Contains(filterText) || e.Street.Contains(filterText) || e.Address.Contains(filterText) || e.Phone.Contains(filterText) || e.License.Contains(filterText) || e.TaxCode.Contains(filterText) || e.VATName.Contains(filterText) || e.VATAddress.Contains(filterText) || e.ERPCode.Contains(filterText) || e.Latitude.Contains(filterText) || e.Longitude.Contains(filterText) || e.ContactName.Contains(filterText) || e.ContactPhone.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(street), e => e.Street.Contains(street))
                    .WhereIf(!string.IsNullOrWhiteSpace(address), e => e.Address.Contains(address))
                    .WhereIf(!string.IsNullOrWhiteSpace(phone), e => e.Phone.Contains(phone))
                    .WhereIf(!string.IsNullOrWhiteSpace(license), e => e.License.Contains(license))
                    .WhereIf(!string.IsNullOrWhiteSpace(taxCode), e => e.TaxCode.Contains(taxCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(vatName), e => e.VATName.Contains(vatName))
                    .WhereIf(!string.IsNullOrWhiteSpace(vatAddress), e => e.VATAddress.Contains(vatAddress))
                    .WhereIf(!string.IsNullOrWhiteSpace(erpCode), e => e.ERPCode.Contains(erpCode))
                    .WhereIf(active.HasValue, e => e.Active == active)
                    .WhereIf(effectiveDateMin.HasValue, e => e.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.EndDate <= endDateMax.Value)
                    .WhereIf(isHO.HasValue, e => e.IsHO == isHO)
                    .WhereIf(!string.IsNullOrWhiteSpace(latitude), e => e.Latitude.Contains(latitude))
                    .WhereIf(!string.IsNullOrWhiteSpace(longitude), e => e.Longitude.Contains(longitude))
                    .WhereIf(!string.IsNullOrWhiteSpace(contactName), e => e.ContactName.Contains(contactName))
                    .WhereIf(!string.IsNullOrWhiteSpace(contactPhone), e => e.ContactPhone.Contains(contactPhone));
        }
    }
}