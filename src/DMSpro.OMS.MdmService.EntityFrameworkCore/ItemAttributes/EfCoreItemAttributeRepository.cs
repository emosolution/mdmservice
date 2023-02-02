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

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public partial class EfCoreItemAttributeRepository : EfCoreRepository<MdmServiceDbContext, ItemAttribute, Guid>, IItemAttributeRepository
    {
        public EfCoreItemAttributeRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<ItemAttribute>> GetListAsync(
            string filterText = null,
            int? attrNoMin = null,
            int? attrNoMax = null,
            string attrName = null,
            int? hierarchyLevelMin = null,
            int? hierarchyLevelMax = null,
            bool? active = null,
            bool? isSellingCategory = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, attrNoMin, attrNoMax, attrName, hierarchyLevelMin, hierarchyLevelMax, active, isSellingCategory);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemAttributeConsts.GetDefaultSorting(false) : sorting);
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
            bool? isSellingCategory = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, attrNoMin, attrNoMax, attrName, hierarchyLevelMin, hierarchyLevelMax, active, isSellingCategory);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<ItemAttribute> ApplyFilter(
            IQueryable<ItemAttribute> query,
            string filterText,
            int? attrNoMin = null,
            int? attrNoMax = null,
            string attrName = null,
            int? hierarchyLevelMin = null,
            int? hierarchyLevelMax = null,
            bool? active = null,
            bool? isSellingCategory = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.AttrName.Contains(filterText))
                    .WhereIf(attrNoMin.HasValue, e => e.AttrNo >= attrNoMin.Value)
                    .WhereIf(attrNoMax.HasValue, e => e.AttrNo <= attrNoMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(attrName), e => e.AttrName.Contains(attrName))
                    .WhereIf(hierarchyLevelMin.HasValue, e => e.HierarchyLevel >= hierarchyLevelMin.Value)
                    .WhereIf(hierarchyLevelMax.HasValue, e => e.HierarchyLevel <= hierarchyLevelMax.Value)
                    .WhereIf(active.HasValue, e => e.Active == active)
                    .WhereIf(isSellingCategory.HasValue, e => e.IsSellingCategory == isSellingCategory);
        }
    }
}