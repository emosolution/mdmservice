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

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public partial class EfCorePricelistAssignmentRepository : EfCoreRepository<MdmServiceDbContext, PricelistAssignment, Guid>, IPricelistAssignmentRepository
    {
        public EfCorePricelistAssignmentRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<PricelistAssignmentWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(pricelistAssignment => new PricelistAssignmentWithNavigationProperties
                {
                    PricelistAssignment = pricelistAssignment,
                    PriceList = dbContext.PriceLists.FirstOrDefault(c => c.Id == pricelistAssignment.PriceListId),
                    CustomerGroup = dbContext.CustomerGroups.FirstOrDefault(c => c.Id == pricelistAssignment.CustomerGroupId)
                }).FirstOrDefault();
        }

        public async Task<List<PricelistAssignmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string description = null,
            Guid? priceListId = null,
            Guid? customerGroupId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, description, priceListId, customerGroupId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PricelistAssignmentConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<PricelistAssignmentWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from pricelistAssignment in (await GetDbSetAsync())
                   join priceList in (await GetDbContextAsync()).PriceLists on pricelistAssignment.PriceListId equals priceList.Id into priceLists
                   from priceList in priceLists.DefaultIfEmpty()
                   join customerGroup in (await GetDbContextAsync()).CustomerGroups on pricelistAssignment.CustomerGroupId equals customerGroup.Id into customerGroups
                   from customerGroup in customerGroups.DefaultIfEmpty()

                   select new PricelistAssignmentWithNavigationProperties
                   {
                       PricelistAssignment = pricelistAssignment,
                       PriceList = priceList,
                       CustomerGroup = customerGroup
                   };
        }

        protected virtual IQueryable<PricelistAssignmentWithNavigationProperties> ApplyFilter(
            IQueryable<PricelistAssignmentWithNavigationProperties> query,
            string filterText,
            string description = null,
            Guid? priceListId = null,
            Guid? customerGroupId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.PricelistAssignment.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.PricelistAssignment.Description.Contains(description))
                    .WhereIf(priceListId != null && priceListId != Guid.Empty, e => e.PriceList != null && e.PriceList.Id == priceListId)
                    .WhereIf(customerGroupId != null && customerGroupId != Guid.Empty, e => e.CustomerGroup != null && e.CustomerGroup.Id == customerGroupId);
        }

        public async Task<List<PricelistAssignment>> GetListAsync(
            string filterText = null,
            string description = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, description);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PricelistAssignmentConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string description = null,
            Guid? priceListId = null,
            Guid? customerGroupId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, description, priceListId, customerGroupId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<PricelistAssignment> ApplyFilter(
            IQueryable<PricelistAssignment> query,
            string filterText,
            string description = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description));
        }
    }
}