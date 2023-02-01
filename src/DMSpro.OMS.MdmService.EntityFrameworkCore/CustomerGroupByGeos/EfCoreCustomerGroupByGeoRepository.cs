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

namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
    public partial class EfCoreCustomerGroupByGeoRepository : EfCoreRepository<MdmServiceDbContext, CustomerGroupByGeo, Guid>, ICustomerGroupByGeoRepository
    {
        public EfCoreCustomerGroupByGeoRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<CustomerGroupByGeoWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(customerGroupByGeo => new CustomerGroupByGeoWithNavigationProperties
                {
                    CustomerGroupByGeo = customerGroupByGeo,
                    CustomerGroup = dbContext.CustomerGroups.FirstOrDefault(c => c.Id == customerGroupByGeo.CustomerGroupId),
                    GeoMaster = dbContext.GeoMasters.FirstOrDefault(c => c.Id == customerGroupByGeo.GeoMasterId)
                }).FirstOrDefault();
        }

        public async Task<List<CustomerGroupByGeoWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            Guid? customerGroupId = null,
            Guid? geoMasterId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, active, effectiveDateMin, effectiveDateMax, customerGroupId, geoMasterId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerGroupByGeoConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CustomerGroupByGeoWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from customerGroupByGeo in (await GetDbSetAsync())
                   join customerGroup in (await GetDbContextAsync()).CustomerGroups on customerGroupByGeo.CustomerGroupId equals customerGroup.Id into customerGroups
                   from customerGroup in customerGroups.DefaultIfEmpty()
                   join geoMaster in (await GetDbContextAsync()).GeoMasters on customerGroupByGeo.GeoMasterId equals geoMaster.Id into geoMasters
                   from geoMaster in geoMasters.DefaultIfEmpty()

                   select new CustomerGroupByGeoWithNavigationProperties
                   {
                       CustomerGroupByGeo = customerGroupByGeo,
                       CustomerGroup = customerGroup,
                       GeoMaster = geoMaster
                   };
        }

        protected virtual IQueryable<CustomerGroupByGeoWithNavigationProperties> ApplyFilter(
            IQueryable<CustomerGroupByGeoWithNavigationProperties> query,
            string filterText,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            Guid? customerGroupId = null,
            Guid? geoMasterId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(active.HasValue, e => e.CustomerGroupByGeo.Active == active)
                    .WhereIf(effectiveDateMin.HasValue, e => e.CustomerGroupByGeo.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.CustomerGroupByGeo.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(customerGroupId != null && customerGroupId != Guid.Empty, e => e.CustomerGroup != null && e.CustomerGroup.Id == customerGroupId)
                    .WhereIf(geoMasterId != null && geoMasterId != Guid.Empty, e => e.GeoMaster != null && e.GeoMaster.Id == geoMasterId);
        }

        public async Task<List<CustomerGroupByGeo>> GetListAsync(
            string filterText = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, active, effectiveDateMin, effectiveDateMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerGroupByGeoConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            Guid? customerGroupId = null,
            Guid? geoMasterId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, active, effectiveDateMin, effectiveDateMax, customerGroupId, geoMasterId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CustomerGroupByGeo> ApplyFilter(
            IQueryable<CustomerGroupByGeo> query,
            string filterText,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(active.HasValue, e => e.Active == active)
                    .WhereIf(effectiveDateMin.HasValue, e => e.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.EffectiveDate <= effectiveDateMax.Value);
        }
    }
}