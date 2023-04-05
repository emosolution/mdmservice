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

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public class EfCoreCustomerGroupGeoRepository : EfCoreRepository<MdmServiceDbContext, CustomerGroupGeo, Guid>, ICustomerGroupGeoRepository
    {
        public EfCoreCustomerGroupGeoRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<CustomerGroupGeoWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(customerGroupGeo => new CustomerGroupGeoWithNavigationProperties
                {
                    CustomerGroupGeo = customerGroupGeo,
                    CustomerGroup = dbContext.CustomerGroups.FirstOrDefault(c => c.Id == customerGroupGeo.CustomerGroupId),
                    GeoMaster = dbContext.GeoMasters.FirstOrDefault(c => c.Id == customerGroupGeo.GeoMaster0Id),
                    GeoMaster1 = dbContext.GeoMasters.FirstOrDefault(c => c.Id == customerGroupGeo.GeoMaster1Id),
                    GeoMaster2 = dbContext.GeoMasters.FirstOrDefault(c => c.Id == customerGroupGeo.GeoMaster2Id),
                    GeoMaster3 = dbContext.GeoMasters.FirstOrDefault(c => c.Id == customerGroupGeo.GeoMaster3Id),
                    GeoMaster4 = dbContext.GeoMasters.FirstOrDefault(c => c.Id == customerGroupGeo.GeoMaster4Id)
                }).FirstOrDefault();
        }

        public async Task<List<CustomerGroupGeoWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string description = null,
            bool? active = null,
            Guid? customerGroupId = null,
            Guid? geoMaster0Id = null,
            Guid? geoMaster1Id = null,
            Guid? geoMaster2Id = null,
            Guid? geoMaster3Id = null,
            Guid? geoMaster4Id = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, description, active, customerGroupId, geoMaster0Id, geoMaster1Id, geoMaster2Id, geoMaster3Id, geoMaster4Id);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerGroupGeoConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CustomerGroupGeoWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from customerGroupGeo in (await GetDbSetAsync())
                   join customerGroup in (await GetDbContextAsync()).CustomerGroups on customerGroupGeo.CustomerGroupId equals customerGroup.Id into customerGroups
                   from customerGroup in customerGroups.DefaultIfEmpty()
                   join geoMaster in (await GetDbContextAsync()).GeoMasters on customerGroupGeo.GeoMaster0Id equals geoMaster.Id into geoMasters
                   from geoMaster in geoMasters.DefaultIfEmpty()
                   join geoMaster1 in (await GetDbContextAsync()).GeoMasters on customerGroupGeo.GeoMaster1Id equals geoMaster1.Id into geoMasters1
                   from geoMaster1 in geoMasters1.DefaultIfEmpty()
                   join geoMaster2 in (await GetDbContextAsync()).GeoMasters on customerGroupGeo.GeoMaster2Id equals geoMaster2.Id into geoMasters2
                   from geoMaster2 in geoMasters2.DefaultIfEmpty()
                   join geoMaster3 in (await GetDbContextAsync()).GeoMasters on customerGroupGeo.GeoMaster3Id equals geoMaster3.Id into geoMasters3
                   from geoMaster3 in geoMasters3.DefaultIfEmpty()
                   join geoMaster4 in (await GetDbContextAsync()).GeoMasters on customerGroupGeo.GeoMaster4Id equals geoMaster4.Id into geoMasters4
                   from geoMaster4 in geoMasters4.DefaultIfEmpty()

                   select new CustomerGroupGeoWithNavigationProperties
                   {
                       CustomerGroupGeo = customerGroupGeo,
                       CustomerGroup = customerGroup,
                       GeoMaster = geoMaster,
                       GeoMaster1 = geoMaster1,
                       GeoMaster2 = geoMaster2,
                       GeoMaster3 = geoMaster3,
                       GeoMaster4 = geoMaster4
                   };
        }

        protected virtual IQueryable<CustomerGroupGeoWithNavigationProperties> ApplyFilter(
            IQueryable<CustomerGroupGeoWithNavigationProperties> query,
            string filterText,
            string description = null,
            bool? active = null,
            Guid? customerGroupId = null,
            Guid? geoMaster0Id = null,
            Guid? geoMaster1Id = null,
            Guid? geoMaster2Id = null,
            Guid? geoMaster3Id = null,
            Guid? geoMaster4Id = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CustomerGroupGeo.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.CustomerGroupGeo.Description.Contains(description))
                    .WhereIf(active.HasValue, e => e.CustomerGroupGeo.Active == active)
                    .WhereIf(customerGroupId != null && customerGroupId != Guid.Empty, e => e.CustomerGroup != null && e.CustomerGroup.Id == customerGroupId)
                    .WhereIf(geoMaster0Id != null && geoMaster0Id != Guid.Empty, e => e.GeoMaster != null && e.GeoMaster.Id == geoMaster0Id)
                    .WhereIf(geoMaster1Id != null && geoMaster1Id != Guid.Empty, e => e.GeoMaster1 != null && e.GeoMaster1.Id == geoMaster1Id)
                    .WhereIf(geoMaster2Id != null && geoMaster2Id != Guid.Empty, e => e.GeoMaster2 != null && e.GeoMaster2.Id == geoMaster2Id)
                    .WhereIf(geoMaster3Id != null && geoMaster3Id != Guid.Empty, e => e.GeoMaster3 != null && e.GeoMaster3.Id == geoMaster3Id)
                    .WhereIf(geoMaster4Id != null && geoMaster4Id != Guid.Empty, e => e.GeoMaster4 != null && e.GeoMaster4.Id == geoMaster4Id);
        }

        public async Task<List<CustomerGroupGeo>> GetListAsync(
            string filterText = null,
            string description = null,
            bool? active = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, description, active);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerGroupGeoConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string description = null,
            bool? active = null,
            Guid? customerGroupId = null,
            Guid? geoMaster0Id = null,
            Guid? geoMaster1Id = null,
            Guid? geoMaster2Id = null,
            Guid? geoMaster3Id = null,
            Guid? geoMaster4Id = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, description, active, customerGroupId, geoMaster0Id, geoMaster1Id, geoMaster2Id, geoMaster3Id, geoMaster4Id);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CustomerGroupGeo> ApplyFilter(
            IQueryable<CustomerGroupGeo> query,
            string filterText,
            string description = null,
            bool? active = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(active.HasValue, e => e.Active == active);
        }
    }
}