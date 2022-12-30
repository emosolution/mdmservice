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

namespace DMSpro.OMS.MdmService.CusAttributeValues
{
    public class EfCoreCusAttributeValueRepository : EfCoreRepository<MdmServiceDbContext, CusAttributeValue, Guid>, ICusAttributeValueRepository
    {
        public EfCoreCusAttributeValueRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<CusAttributeValueWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(cusAttributeValue => new CusAttributeValueWithNavigationProperties
                {
                    CusAttributeValue = cusAttributeValue,
                    CustomerAttribute = dbContext.CustomerAttributes.FirstOrDefault(c => c.Id == cusAttributeValue.CustomerAttributeId),
                    CusAttributeValue1 = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == cusAttributeValue.ParentCusAttributeValueId)
                }).FirstOrDefault();
        }

        public async Task<List<CusAttributeValueWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string attrValName = null,
            Guid? customerAttributeId = null,
            Guid? parentCusAttributeValueId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, attrValName, customerAttributeId, parentCusAttributeValueId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CusAttributeValueConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CusAttributeValueWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from cusAttributeValue in (await GetDbSetAsync())
                   join customerAttribute in (await GetDbContextAsync()).CustomerAttributes on cusAttributeValue.CustomerAttributeId equals customerAttribute.Id into customerAttributes
                   from customerAttribute in customerAttributes.DefaultIfEmpty()
                   join cusAttributeValue1 in (await GetDbContextAsync()).CusAttributeValues on cusAttributeValue.ParentCusAttributeValueId equals cusAttributeValue1.Id into cusAttributeValues1
                   from cusAttributeValue1 in cusAttributeValues1.DefaultIfEmpty()

                   select new CusAttributeValueWithNavigationProperties
                   {
                       CusAttributeValue = cusAttributeValue,
                       CustomerAttribute = customerAttribute,
                       CusAttributeValue1 = cusAttributeValue1
                   };
        }

        protected virtual IQueryable<CusAttributeValueWithNavigationProperties> ApplyFilter(
            IQueryable<CusAttributeValueWithNavigationProperties> query,
            string filterText,
            string attrValName = null,
            Guid? customerAttributeId = null,
            Guid? parentCusAttributeValueId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CusAttributeValue.AttrValName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(attrValName), e => e.CusAttributeValue.AttrValName.Contains(attrValName))
                    .WhereIf(customerAttributeId != null && customerAttributeId != Guid.Empty, e => e.CustomerAttribute != null && e.CustomerAttribute.Id == customerAttributeId)
                    .WhereIf(parentCusAttributeValueId != null && parentCusAttributeValueId != Guid.Empty, e => e.CusAttributeValue1 != null && e.CusAttributeValue1.Id == parentCusAttributeValueId);
        }

        public async Task<List<CusAttributeValue>> GetListAsync(
            string filterText = null,
            string attrValName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, attrValName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CusAttributeValueConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string attrValName = null,
            Guid? customerAttributeId = null,
            Guid? parentCusAttributeValueId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, attrValName, customerAttributeId, parentCusAttributeValueId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CusAttributeValue> ApplyFilter(
            IQueryable<CusAttributeValue> query,
            string filterText,
            string attrValName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.AttrValName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(attrValName), e => e.AttrValName.Contains(attrValName));
        }
    }
}