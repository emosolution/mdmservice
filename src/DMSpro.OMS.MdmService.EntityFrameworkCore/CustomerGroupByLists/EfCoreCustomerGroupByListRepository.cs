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

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
    public partial class EfCoreCustomerGroupByListRepository : EfCoreRepository<MdmServiceDbContext, CustomerGroupByList, Guid>, ICustomerGroupByListRepository
    {
        public EfCoreCustomerGroupByListRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<CustomerGroupByListWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(customerGroupByList => new CustomerGroupByListWithNavigationProperties
                {
                    CustomerGroupByList = customerGroupByList,
                    CustomerGroup = dbContext.CustomerGroups.FirstOrDefault(c => c.Id == customerGroupByList.CustomerGroupId),
                    Customer = dbContext.Customers.FirstOrDefault(c => c.Id == customerGroupByList.CustomerId)
                }).FirstOrDefault();
        }

        public async Task<List<CustomerGroupByListWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            bool? active = null,
            Guid? customerGroupId = null,
            Guid? customerId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, active, customerGroupId, customerId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerGroupByListConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CustomerGroupByListWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from customerGroupByList in (await GetDbSetAsync())
                   join customerGroup in (await GetDbContextAsync()).CustomerGroups on customerGroupByList.CustomerGroupId equals customerGroup.Id into customerGroups
                   from customerGroup in customerGroups.DefaultIfEmpty()
                   join customer in (await GetDbContextAsync()).Customers on customerGroupByList.CustomerId equals customer.Id into customers
                   from customer in customers.DefaultIfEmpty()

                   select new CustomerGroupByListWithNavigationProperties
                   {
                       CustomerGroupByList = customerGroupByList,
                       CustomerGroup = customerGroup,
                       Customer = customer
                   };
        }

        protected virtual IQueryable<CustomerGroupByListWithNavigationProperties> ApplyFilter(
            IQueryable<CustomerGroupByListWithNavigationProperties> query,
            string filterText,
            bool? active = null,
            Guid? customerGroupId = null,
            Guid? customerId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(active.HasValue, e => e.CustomerGroupByList.Active == active)
                    .WhereIf(customerGroupId != null && customerGroupId != Guid.Empty, e => e.CustomerGroup != null && e.CustomerGroup.Id == customerGroupId)
                    .WhereIf(customerId != null && customerId != Guid.Empty, e => e.Customer != null && e.Customer.Id == customerId);
        }

        public async Task<List<CustomerGroupByList>> GetListAsync(
            string filterText = null,
            bool? active = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, active);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerGroupByListConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            bool? active = null,
            Guid? customerGroupId = null,
            Guid? customerId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, active, customerGroupId, customerId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CustomerGroupByList> ApplyFilter(
            IQueryable<CustomerGroupByList> query,
            string filterText,
            bool? active = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(active.HasValue, e => e.Active == active);
        }
    }
}