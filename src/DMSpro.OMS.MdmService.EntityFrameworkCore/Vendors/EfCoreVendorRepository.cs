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

namespace DMSpro.OMS.MdmService.Vendors
{
    public class EfCoreVendorRepository : EfCoreRepository<MdmServiceDbContext, Vendor, Guid>, IVendorRepository
    {
        public EfCoreVendorRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<VendorWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(vendor => new VendorWithNavigationProperties
                {
                    Vendor = vendor,
                    PriceList = dbContext.PriceLists.FirstOrDefault(c => c.Id == vendor.PriceListId),
                    GeoMaster = dbContext.GeoMasters.FirstOrDefault(c => c.Id == vendor.GeoMaster0Id),
                    GeoMaster1 = dbContext.GeoMasters.FirstOrDefault(c => c.Id == vendor.GeoMaster1Id),
                    GeoMaster2 = dbContext.GeoMasters.FirstOrDefault(c => c.Id == vendor.GeoMaster2Id),
                    GeoMaster3 = dbContext.GeoMasters.FirstOrDefault(c => c.Id == vendor.GeoMaster3Id),
                    GeoMaster4 = dbContext.GeoMasters.FirstOrDefault(c => c.Id == vendor.GeoMaster4Id),
                    Company = dbContext.Companies.FirstOrDefault(c => c.Id == vendor.CompanyId)
                }).FirstOrDefault();
        }

        public async Task<List<VendorWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string shortName = null,
            string phone1 = null,
            string phone2 = null,
            string erpCode = null,
            bool? active = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            string linkedCompany = null,
            Guid? warehouseId = null,
            string street = null,
            string address = null,
            string latitude = null,
            string longitude = null,
            Guid? priceListId = null,
            Guid? geoMaster0Id = null,
            Guid? geoMaster1Id = null,
            Guid? geoMaster2Id = null,
            Guid? geoMaster3Id = null,
            Guid? geoMaster4Id = null,
            Guid? companyId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, shortName, phone1, phone2, erpCode, active, endDateMin, endDateMax, linkedCompany, warehouseId, street, address, latitude, longitude, priceListId, geoMaster0Id, geoMaster1Id, geoMaster2Id, geoMaster3Id, geoMaster4Id, companyId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VendorConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<VendorWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from vendor in (await GetDbSetAsync())
                   join priceList in (await GetDbContextAsync()).PriceLists on vendor.PriceListId equals priceList.Id into priceLists
                   from priceList in priceLists.DefaultIfEmpty()
                   join geoMaster in (await GetDbContextAsync()).GeoMasters on vendor.GeoMaster0Id equals geoMaster.Id into geoMasters
                   from geoMaster in geoMasters.DefaultIfEmpty()
                   join geoMaster1 in (await GetDbContextAsync()).GeoMasters on vendor.GeoMaster1Id equals geoMaster1.Id into geoMasters1
                   from geoMaster1 in geoMasters1.DefaultIfEmpty()
                   join geoMaster2 in (await GetDbContextAsync()).GeoMasters on vendor.GeoMaster2Id equals geoMaster2.Id into geoMasters2
                   from geoMaster2 in geoMasters2.DefaultIfEmpty()
                   join geoMaster3 in (await GetDbContextAsync()).GeoMasters on vendor.GeoMaster3Id equals geoMaster3.Id into geoMasters3
                   from geoMaster3 in geoMasters3.DefaultIfEmpty()
                   join geoMaster4 in (await GetDbContextAsync()).GeoMasters on vendor.GeoMaster4Id equals geoMaster4.Id into geoMasters4
                   from geoMaster4 in geoMasters4.DefaultIfEmpty()
                   join company in (await GetDbContextAsync()).Companies on vendor.CompanyId equals company.Id into companies
                   from company in companies.DefaultIfEmpty()

                   select new VendorWithNavigationProperties
                   {
                       Vendor = vendor,
                       PriceList = priceList,
                       GeoMaster = geoMaster,
                       GeoMaster1 = geoMaster1,
                       GeoMaster2 = geoMaster2,
                       GeoMaster3 = geoMaster3,
                       GeoMaster4 = geoMaster4,
                       Company = company
                   };
        }

        protected virtual IQueryable<VendorWithNavigationProperties> ApplyFilter(
            IQueryable<VendorWithNavigationProperties> query,
            string filterText,
            string code = null,
            string name = null,
            string shortName = null,
            string phone1 = null,
            string phone2 = null,
            string erpCode = null,
            bool? active = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            string linkedCompany = null,
            Guid? warehouseId = null,
            string street = null,
            string address = null,
            string latitude = null,
            string longitude = null,
            Guid? priceListId = null,
            Guid? geoMaster0Id = null,
            Guid? geoMaster1Id = null,
            Guid? geoMaster2Id = null,
            Guid? geoMaster3Id = null,
            Guid? geoMaster4Id = null,
            Guid? companyId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Vendor.Code.Contains(filterText) || e.Vendor.Name.Contains(filterText) || e.Vendor.ShortName.Contains(filterText) || e.Vendor.Phone1.Contains(filterText) || e.Vendor.Phone2.Contains(filterText) || e.Vendor.ERPCode.Contains(filterText) || e.Vendor.LinkedCompany.Contains(filterText) || e.Vendor.Street.Contains(filterText) || e.Vendor.Address.Contains(filterText) || e.Vendor.Latitude.Contains(filterText) || e.Vendor.Longitude.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Vendor.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Vendor.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(shortName), e => e.Vendor.ShortName.Contains(shortName))
                    .WhereIf(!string.IsNullOrWhiteSpace(phone1), e => e.Vendor.Phone1.Contains(phone1))
                    .WhereIf(!string.IsNullOrWhiteSpace(phone2), e => e.Vendor.Phone2.Contains(phone2))
                    .WhereIf(!string.IsNullOrWhiteSpace(erpCode), e => e.Vendor.ERPCode.Contains(erpCode))
                    .WhereIf(active.HasValue, e => e.Vendor.Active == active)
                    .WhereIf(endDateMin.HasValue, e => e.Vendor.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.Vendor.EndDate <= endDateMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(linkedCompany), e => e.Vendor.LinkedCompany.Contains(linkedCompany))
                    .WhereIf(warehouseId.HasValue, e => e.Vendor.WarehouseId == warehouseId)
                    .WhereIf(!string.IsNullOrWhiteSpace(street), e => e.Vendor.Street.Contains(street))
                    .WhereIf(!string.IsNullOrWhiteSpace(address), e => e.Vendor.Address.Contains(address))
                    .WhereIf(!string.IsNullOrWhiteSpace(latitude), e => e.Vendor.Latitude.Contains(latitude))
                    .WhereIf(!string.IsNullOrWhiteSpace(longitude), e => e.Vendor.Longitude.Contains(longitude))
                    .WhereIf(priceListId != null && priceListId != Guid.Empty, e => e.PriceList != null && e.PriceList.Id == priceListId)
                    .WhereIf(geoMaster0Id != null && geoMaster0Id != Guid.Empty, e => e.GeoMaster != null && e.GeoMaster.Id == geoMaster0Id)
                    .WhereIf(geoMaster1Id != null && geoMaster1Id != Guid.Empty, e => e.GeoMaster1 != null && e.GeoMaster1.Id == geoMaster1Id)
                    .WhereIf(geoMaster2Id != null && geoMaster2Id != Guid.Empty, e => e.GeoMaster2 != null && e.GeoMaster2.Id == geoMaster2Id)
                    .WhereIf(geoMaster3Id != null && geoMaster3Id != Guid.Empty, e => e.GeoMaster3 != null && e.GeoMaster3.Id == geoMaster3Id)
                    .WhereIf(geoMaster4Id != null && geoMaster4Id != Guid.Empty, e => e.GeoMaster4 != null && e.GeoMaster4.Id == geoMaster4Id)
                    .WhereIf(companyId != null && companyId != Guid.Empty, e => e.Company != null && e.Company.Id == companyId);
        }

        public async Task<List<Vendor>> GetListAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string shortName = null,
            string phone1 = null,
            string phone2 = null,
            string erpCode = null,
            bool? active = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            string linkedCompany = null,
            Guid? warehouseId = null,
            string street = null,
            string address = null,
            string latitude = null,
            string longitude = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, name, shortName, phone1, phone2, erpCode, active, endDateMin, endDateMax, linkedCompany, warehouseId, street, address, latitude, longitude);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? VendorConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string shortName = null,
            string phone1 = null,
            string phone2 = null,
            string erpCode = null,
            bool? active = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            string linkedCompany = null,
            Guid? warehouseId = null,
            string street = null,
            string address = null,
            string latitude = null,
            string longitude = null,
            Guid? priceListId = null,
            Guid? geoMaster0Id = null,
            Guid? geoMaster1Id = null,
            Guid? geoMaster2Id = null,
            Guid? geoMaster3Id = null,
            Guid? geoMaster4Id = null,
            Guid? companyId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, shortName, phone1, phone2, erpCode, active, endDateMin, endDateMax, linkedCompany, warehouseId, street, address, latitude, longitude, priceListId, geoMaster0Id, geoMaster1Id, geoMaster2Id, geoMaster3Id, geoMaster4Id, companyId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Vendor> ApplyFilter(
            IQueryable<Vendor> query,
            string filterText,
            string code = null,
            string name = null,
            string shortName = null,
            string phone1 = null,
            string phone2 = null,
            string erpCode = null,
            bool? active = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            string linkedCompany = null,
            Guid? warehouseId = null,
            string street = null,
            string address = null,
            string latitude = null,
            string longitude = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.Name.Contains(filterText) || e.ShortName.Contains(filterText) || e.Phone1.Contains(filterText) || e.Phone2.Contains(filterText) || e.ERPCode.Contains(filterText) || e.LinkedCompany.Contains(filterText) || e.Street.Contains(filterText) || e.Address.Contains(filterText) || e.Latitude.Contains(filterText) || e.Longitude.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(shortName), e => e.ShortName.Contains(shortName))
                    .WhereIf(!string.IsNullOrWhiteSpace(phone1), e => e.Phone1.Contains(phone1))
                    .WhereIf(!string.IsNullOrWhiteSpace(phone2), e => e.Phone2.Contains(phone2))
                    .WhereIf(!string.IsNullOrWhiteSpace(erpCode), e => e.ERPCode.Contains(erpCode))
                    .WhereIf(active.HasValue, e => e.Active == active)
                    .WhereIf(endDateMin.HasValue, e => e.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.EndDate <= endDateMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(linkedCompany), e => e.LinkedCompany.Contains(linkedCompany))
                    .WhereIf(warehouseId.HasValue, e => e.WarehouseId == warehouseId)
                    .WhereIf(!string.IsNullOrWhiteSpace(street), e => e.Street.Contains(street))
                    .WhereIf(!string.IsNullOrWhiteSpace(address), e => e.Address.Contains(address))
                    .WhereIf(!string.IsNullOrWhiteSpace(latitude), e => e.Latitude.Contains(latitude))
                    .WhereIf(!string.IsNullOrWhiteSpace(longitude), e => e.Longitude.Contains(longitude));
        }
    }
}