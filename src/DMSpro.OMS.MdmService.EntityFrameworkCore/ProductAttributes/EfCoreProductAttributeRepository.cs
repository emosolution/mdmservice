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

namespace DMSpro.OMS.MdmService.ProductAttributes
{
    public class EfCoreProductAttributeRepository : EfCoreRepository<MdmServiceDbContext, ProductAttribute, Guid>, IProductAttributeRepository
    {
        public EfCoreProductAttributeRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<ProductAttribute>> GetListAsync(
            string filterText = null,
            int? attrNoMin = null,
            int? attrNoMax = null,
            string attrName = null,
            int? hierarchyLevelMin = null,
            int? hierarchyLevelMax = null,
            bool? active = null,
            bool? isProductCategory = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, attrNoMin, attrNoMax, attrName, hierarchyLevelMin, hierarchyLevelMax, active, isProductCategory);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ProductAttributeConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            int? attrNoMin = null,
            int? attrNoMax = null,
            string attrName = null,
            int? hierarchyLevelMin = null,
            int? hierarchyLevelMax = null,
            bool? active = null,
            bool? isProductCategory = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, attrNoMin, attrNoMax, attrName, hierarchyLevelMin, hierarchyLevelMax, active, isProductCategory);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<ProductAttribute> ApplyFilter(
            IQueryable<ProductAttribute> query,
            string filterText,
            int? attrNoMin = null,
            int? attrNoMax = null,
            string attrName = null,
            int? hierarchyLevelMin = null,
            int? hierarchyLevelMax = null,
            bool? active = null,
            bool? isProductCategory = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.AttrName.Contains(filterText))
                    .WhereIf(attrNoMin.HasValue, e => e.AttrNo >= attrNoMin.Value)
                    .WhereIf(attrNoMax.HasValue, e => e.AttrNo <= attrNoMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(attrName), e => e.AttrName.Contains(attrName))
                    .WhereIf(hierarchyLevelMin.HasValue, e => e.HierarchyLevel >= hierarchyLevelMin.Value)
                    .WhereIf(hierarchyLevelMax.HasValue, e => e.HierarchyLevel <= hierarchyLevelMax.Value)
                    .WhereIf(active.HasValue, e => e.Active == active)
                    .WhereIf(isProductCategory.HasValue, e => e.IsProductCategory == isProductCategory);
        }

        public async Task<bool> CreateWithExcepAsync(List<ProductAttribute> seedData)
        {
            await this.InsertManyAsync(seedData);
            return true;
        }
    }
}