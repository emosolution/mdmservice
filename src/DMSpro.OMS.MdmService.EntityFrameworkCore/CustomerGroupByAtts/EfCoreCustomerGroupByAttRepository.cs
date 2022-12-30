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

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
    public class EfCoreCustomerGroupByAttRepository : EfCoreRepository<MdmServiceDbContext, CustomerGroupByAtt, Guid>, ICustomerGroupByAttRepository
    {
        public EfCoreCustomerGroupByAttRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<CustomerGroupByAttWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(customerGroupByAtt => new CustomerGroupByAttWithNavigationProperties
                {
                    CustomerGroupByAtt = customerGroupByAtt,
                    CustomerGroup = dbContext.CustomerGroups.FirstOrDefault(c => c.Id == customerGroupByAtt.CustomerGroupId),
                    CusAttributeValue = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == customerGroupByAtt.CusAttributeValueId)
                }).FirstOrDefault();
        }

        public async Task<List<CustomerGroupByAttWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string valueCode = null,
            string valueName = null,
            Guid? customerGroupId = null,
            Guid? cusAttributeValueId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, valueCode, valueName, customerGroupId, cusAttributeValueId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerGroupByAttConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CustomerGroupByAttWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from customerGroupByAtt in (await GetDbSetAsync())
                   join customerGroup in (await GetDbContextAsync()).CustomerGroups on customerGroupByAtt.CustomerGroupId equals customerGroup.Id into customerGroups
                   from customerGroup in customerGroups.DefaultIfEmpty()
                   join cusAttributeValue in (await GetDbContextAsync()).CusAttributeValues on customerGroupByAtt.CusAttributeValueId equals cusAttributeValue.Id into cusAttributeValues
                   from cusAttributeValue in cusAttributeValues.DefaultIfEmpty()

                   select new CustomerGroupByAttWithNavigationProperties
                   {
                       CustomerGroupByAtt = customerGroupByAtt,
                       CustomerGroup = customerGroup,
                       CusAttributeValue = cusAttributeValue
                   };
        }

        protected virtual IQueryable<CustomerGroupByAttWithNavigationProperties> ApplyFilter(
            IQueryable<CustomerGroupByAttWithNavigationProperties> query,
            string filterText,
            string valueCode = null,
            string valueName = null,
            Guid? customerGroupId = null,
            Guid? cusAttributeValueId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CustomerGroupByAtt.ValueCode.Contains(filterText) || e.CustomerGroupByAtt.ValueName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(valueCode), e => e.CustomerGroupByAtt.ValueCode.Contains(valueCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(valueName), e => e.CustomerGroupByAtt.ValueName.Contains(valueName))
                    .WhereIf(customerGroupId != null && customerGroupId != Guid.Empty, e => e.CustomerGroup != null && e.CustomerGroup.Id == customerGroupId)
                    .WhereIf(cusAttributeValueId != null && cusAttributeValueId != Guid.Empty, e => e.CusAttributeValue != null && e.CusAttributeValue.Id == cusAttributeValueId);
        }

        public async Task<List<CustomerGroupByAtt>> GetListAsync(
            string filterText = null,
            string valueCode = null,
            string valueName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, valueCode, valueName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerGroupByAttConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string valueCode = null,
            string valueName = null,
            Guid? customerGroupId = null,
            Guid? cusAttributeValueId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, valueCode, valueName, customerGroupId, cusAttributeValueId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CustomerGroupByAtt> ApplyFilter(
            IQueryable<CustomerGroupByAtt> query,
            string filterText,
            string valueCode = null,
            string valueName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ValueCode.Contains(filterText) || e.ValueName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(valueCode), e => e.ValueCode.Contains(valueCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(valueName), e => e.ValueName.Contains(valueName));
        }
    }
}