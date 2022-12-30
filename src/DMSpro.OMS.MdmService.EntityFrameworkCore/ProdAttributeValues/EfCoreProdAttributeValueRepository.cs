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

namespace DMSpro.OMS.MdmService.ProdAttributeValues
{
    public class EfCoreProdAttributeValueRepository : EfCoreRepository<MdmServiceDbContext, ProdAttributeValue, Guid>, IProdAttributeValueRepository
    {
        public EfCoreProdAttributeValueRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<ProdAttributeValueWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(prodAttributeValue => new ProdAttributeValueWithNavigationProperties
                {
                    ProdAttributeValue = prodAttributeValue,
                    ProductAttribute = dbContext.ProductAttributes.FirstOrDefault(c => c.Id == prodAttributeValue.ProdAttributeId),
                    ProdAttributeValue1 = dbContext.ProdAttributeValues.FirstOrDefault(c => c.Id == prodAttributeValue.ParentProdAttributeValueId)
                }).FirstOrDefault();
        }

        public async Task<List<ProdAttributeValueWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string attrValName = null,
            Guid? prodAttributeId = null,
            Guid? parentProdAttributeValueId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, attrValName, prodAttributeId, parentProdAttributeValueId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ProdAttributeValueConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ProdAttributeValueWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from prodAttributeValue in (await GetDbSetAsync())
                   join productAttribute in (await GetDbContextAsync()).ProductAttributes on prodAttributeValue.ProdAttributeId equals productAttribute.Id into productAttributes
                   from productAttribute in productAttributes.DefaultIfEmpty()
                   join prodAttributeValue1 in (await GetDbContextAsync()).ProdAttributeValues on prodAttributeValue.ParentProdAttributeValueId equals prodAttributeValue1.Id into prodAttributeValues1
                   from prodAttributeValue1 in prodAttributeValues1.DefaultIfEmpty()

                   select new ProdAttributeValueWithNavigationProperties
                   {
                       ProdAttributeValue = prodAttributeValue,
                       ProductAttribute = productAttribute,
                       ProdAttributeValue1 = prodAttributeValue1
                   };
        }

        protected virtual IQueryable<ProdAttributeValueWithNavigationProperties> ApplyFilter(
            IQueryable<ProdAttributeValueWithNavigationProperties> query,
            string filterText,
            string attrValName = null,
            Guid? prodAttributeId = null,
            Guid? parentProdAttributeValueId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ProdAttributeValue.AttrValName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(attrValName), e => e.ProdAttributeValue.AttrValName.Contains(attrValName))
                    .WhereIf(prodAttributeId != null && prodAttributeId != Guid.Empty, e => e.ProductAttribute != null && e.ProductAttribute.Id == prodAttributeId)
                    .WhereIf(parentProdAttributeValueId != null && parentProdAttributeValueId != Guid.Empty, e => e.ProdAttributeValue1 != null && e.ProdAttributeValue1.Id == parentProdAttributeValueId);
        }

        public async Task<List<ProdAttributeValue>> GetListAsync(
            string filterText = null,
            string attrValName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, attrValName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ProdAttributeValueConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string attrValName = null,
            Guid? prodAttributeId = null,
            Guid? parentProdAttributeValueId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, attrValName, prodAttributeId, parentProdAttributeValueId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<ProdAttributeValue> ApplyFilter(
            IQueryable<ProdAttributeValue> query,
            string filterText,
            string attrValName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.AttrValName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(attrValName), e => e.AttrValName.Contains(attrValName));
        }
    }
}