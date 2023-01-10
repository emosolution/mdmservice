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

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
    public class EfCoreCustomerAttributeRepository : EfCoreRepository<MdmServiceDbContext, CustomerAttribute, Guid>, ICustomerAttributeRepository
    {
        public EfCoreCustomerAttributeRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<CustomerAttribute>> GetListAsync(
            string filterText = null,
            int? attrNoMin = null,
            int? attrNoMax = null,
            string attrName = null,
            int? hierarchyLevelMin = null,
            int? hierarchyLevelMax = null,
            bool? active = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, attrNoMin, attrNoMax, attrName, hierarchyLevelMin, hierarchyLevelMax, active);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerAttributeConsts.GetDefaultSorting(false) : sorting);
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
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, attrNoMin, attrNoMax, attrName, hierarchyLevelMin, hierarchyLevelMax, active);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CustomerAttribute> ApplyFilter(
            IQueryable<CustomerAttribute> query,
            string filterText,
            int? attrNoMin = null,
            int? attrNoMax = null,
            string attrName = null,
            int? hierarchyLevelMin = null,
            int? hierarchyLevelMax = null,
            bool? active = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.AttrName.Contains(filterText))
                    .WhereIf(attrNoMin.HasValue, e => e.AttrNo >= attrNoMin.Value)
                    .WhereIf(attrNoMax.HasValue, e => e.AttrNo <= attrNoMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(attrName), e => e.AttrName.Contains(attrName))
                    .WhereIf(hierarchyLevelMin.HasValue, e => e.HierarchyLevel >= hierarchyLevelMin.Value)
                    .WhereIf(hierarchyLevelMax.HasValue, e => e.HierarchyLevel <= hierarchyLevelMax.Value)
                    .WhereIf(active.HasValue, e => e.Active == active);
        }

        public async Task<bool> CreateWithExcepAsync(List<CustomerAttribute> seedData)
        {
            await this.InsertManyAsync(seedData);
            return true;
        }
    }
}