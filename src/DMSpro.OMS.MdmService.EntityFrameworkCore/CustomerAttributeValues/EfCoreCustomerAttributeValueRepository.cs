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

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    public class EfCoreCustomerAttributeValueRepository : EfCoreRepository<MdmServiceDbContext, CustomerAttributeValue, Guid>, ICustomerAttributeValueRepository
    {
        public EfCoreCustomerAttributeValueRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<CustomerAttributeValueWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(customerAttributeValue => new CustomerAttributeValueWithNavigationProperties
                {
                    CustomerAttributeValue = customerAttributeValue,
                    CustomerAttribute = dbContext.CustomerAttributes.FirstOrDefault(c => c.Id == customerAttributeValue.CustomerAttributeId),
                    CustomerAttributeValue1 = dbContext.CustomerAttributeValues.FirstOrDefault(c => c.Id == customerAttributeValue.ParentId)
                }).FirstOrDefault();
        }

        public async Task<List<CustomerAttributeValueWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string code = null,
            string attrValName = null,
            Guid? customerAttributeId = null,
            Guid? parentId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, attrValName, customerAttributeId, parentId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerAttributeValueConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CustomerAttributeValueWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from customerAttributeValue in (await GetDbSetAsync())
                   join customerAttribute in (await GetDbContextAsync()).CustomerAttributes on customerAttributeValue.CustomerAttributeId equals customerAttribute.Id into customerAttributes
                   from customerAttribute in customerAttributes.DefaultIfEmpty()
                   join customerAttributeValue1 in (await GetDbContextAsync()).CustomerAttributeValues on customerAttributeValue.ParentId equals customerAttributeValue1.Id into customerAttributeValues1
                   from customerAttributeValue1 in customerAttributeValues1.DefaultIfEmpty()

                   select new CustomerAttributeValueWithNavigationProperties
                   {
                       CustomerAttributeValue = customerAttributeValue,
                       CustomerAttribute = customerAttribute,
                       CustomerAttributeValue1 = customerAttributeValue1
                   };
        }

        protected virtual IQueryable<CustomerAttributeValueWithNavigationProperties> ApplyFilter(
            IQueryable<CustomerAttributeValueWithNavigationProperties> query,
            string filterText,
            string code = null,
            string attrValName = null,
            Guid? customerAttributeId = null,
            Guid? parentId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CustomerAttributeValue.Code.Contains(filterText) || e.CustomerAttributeValue.AttrValName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.CustomerAttributeValue.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(attrValName), e => e.CustomerAttributeValue.AttrValName.Contains(attrValName))
                    .WhereIf(customerAttributeId != null && customerAttributeId != Guid.Empty, e => e.CustomerAttribute != null && e.CustomerAttribute.Id == customerAttributeId)
                    .WhereIf(parentId != null && parentId != Guid.Empty, e => e.CustomerAttributeValue1 != null && e.CustomerAttributeValue1.Id == parentId);
        }

        public async Task<List<CustomerAttributeValue>> GetListAsync(
            string filterText = null,
            string code = null,
            string attrValName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, attrValName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerAttributeValueConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string attrValName = null,
            Guid? customerAttributeId = null,
            Guid? parentId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, attrValName, customerAttributeId, parentId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CustomerAttributeValue> ApplyFilter(
            IQueryable<CustomerAttributeValue> query,
            string filterText,
            string code = null,
            string attrValName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.AttrValName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(attrValName), e => e.AttrValName.Contains(attrValName));
        }
    }
}