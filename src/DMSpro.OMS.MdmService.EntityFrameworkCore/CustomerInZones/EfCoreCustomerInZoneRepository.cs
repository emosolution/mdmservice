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

namespace DMSpro.OMS.MdmService.CustomerInZones
{
    public partial class EfCoreCustomerInZoneRepository : EfCoreRepository<MdmServiceDbContext, CustomerInZone, Guid>, ICustomerInZoneRepository
    {
        public EfCoreCustomerInZoneRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<CustomerInZoneWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(customerInZone => new CustomerInZoneWithNavigationProperties
                {
                    CustomerInZone = customerInZone,
                    SalesOrgHierarchy = dbContext.SalesOrgHierarchies.FirstOrDefault(c => c.Id == customerInZone.SalesOrgHierarchyId),
                    Customer = dbContext.Customers.FirstOrDefault(c => c.Id == customerInZone.CustomerId)
                }).FirstOrDefault();
        }

        public async Task<List<CustomerInZoneWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            Guid? salesOrgHierarchyId = null,
            Guid? customerId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, salesOrgHierarchyId, customerId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerInZoneConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CustomerInZoneWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from customerInZone in (await GetDbSetAsync())
                   join salesOrgHierarchy in (await GetDbContextAsync()).SalesOrgHierarchies on customerInZone.SalesOrgHierarchyId equals salesOrgHierarchy.Id into salesOrgHierarchies
                   from salesOrgHierarchy in salesOrgHierarchies.DefaultIfEmpty()
                   join customer in (await GetDbContextAsync()).Customers on customerInZone.CustomerId equals customer.Id into customers
                   from customer in customers.DefaultIfEmpty()

                   select new CustomerInZoneWithNavigationProperties
                   {
                       CustomerInZone = customerInZone,
                       SalesOrgHierarchy = salesOrgHierarchy,
                       Customer = customer
                   };
        }

        protected virtual IQueryable<CustomerInZoneWithNavigationProperties> ApplyFilter(
            IQueryable<CustomerInZoneWithNavigationProperties> query,
            string filterText,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            Guid? salesOrgHierarchyId = null,
            Guid? customerId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(effectiveDateMin.HasValue, e => e.CustomerInZone.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.CustomerInZone.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.CustomerInZone.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.CustomerInZone.EndDate <= endDateMax.Value)
                    .WhereIf(salesOrgHierarchyId != null && salesOrgHierarchyId != Guid.Empty, e => e.SalesOrgHierarchy != null && e.SalesOrgHierarchy.Id == salesOrgHierarchyId)
                    .WhereIf(customerId != null && customerId != Guid.Empty, e => e.Customer != null && e.Customer.Id == customerId);
        }

        public async Task<List<CustomerInZone>> GetListAsync(
            string filterText = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerInZoneConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            Guid? salesOrgHierarchyId = null,
            Guid? customerId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, salesOrgHierarchyId, customerId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CustomerInZone> ApplyFilter(
            IQueryable<CustomerInZone> query,
            string filterText,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(effectiveDateMin.HasValue, e => e.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.EndDate <= endDateMax.Value);
        }
    }
}