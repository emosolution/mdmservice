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

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public class EfCoreCustomerGroupListRepository : EfCoreRepository<MdmServiceDbContext, CustomerGroupList, Guid>, ICustomerGroupListRepository
    {
        public EfCoreCustomerGroupListRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<CustomerGroupListWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(customerGroupList => new CustomerGroupListWithNavigationProperties
                {
                    CustomerGroupList = customerGroupList,
                    Customer = dbContext.Customers.FirstOrDefault(c => c.Id == customerGroupList.CustomerId),
                    CustomerGroup = dbContext.CustomerGroups.FirstOrDefault(c => c.Id == customerGroupList.CustomerGroupId)
                }).FirstOrDefault();
        }

        public async Task<List<CustomerGroupListWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string description = null,
            bool? active = null,
            Guid? customerId = null,
            Guid? customerGroupId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, description, active, customerId, customerGroupId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerGroupListConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CustomerGroupListWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from customerGroupList in (await GetDbSetAsync())
                   join customer in (await GetDbContextAsync()).Customers on customerGroupList.CustomerId equals customer.Id into customers
                   from customer in customers.DefaultIfEmpty()
                   join customerGroup in (await GetDbContextAsync()).CustomerGroups on customerGroupList.CustomerGroupId equals customerGroup.Id into customerGroups
                   from customerGroup in customerGroups.DefaultIfEmpty()

                   select new CustomerGroupListWithNavigationProperties
                   {
                       CustomerGroupList = customerGroupList,
                       Customer = customer,
                       CustomerGroup = customerGroup
                   };
        }

        protected virtual IQueryable<CustomerGroupListWithNavigationProperties> ApplyFilter(
            IQueryable<CustomerGroupListWithNavigationProperties> query,
            string filterText,
            string description = null,
            bool? active = null,
            Guid? customerId = null,
            Guid? customerGroupId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CustomerGroupList.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.CustomerGroupList.Description.Contains(description))
                    .WhereIf(active.HasValue, e => e.CustomerGroupList.Active == active)
                    .WhereIf(customerId != null && customerId != Guid.Empty, e => e.Customer != null && e.Customer.Id == customerId)
                    .WhereIf(customerGroupId != null && customerGroupId != Guid.Empty, e => e.CustomerGroup != null && e.CustomerGroup.Id == customerGroupId);
        }

        public async Task<List<CustomerGroupList>> GetListAsync(
            string filterText = null,
            string description = null,
            bool? active = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, description, active);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerGroupListConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string description = null,
            bool? active = null,
            Guid? customerId = null,
            Guid? customerGroupId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, description, active, customerId, customerGroupId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CustomerGroupList> ApplyFilter(
            IQueryable<CustomerGroupList> query,
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