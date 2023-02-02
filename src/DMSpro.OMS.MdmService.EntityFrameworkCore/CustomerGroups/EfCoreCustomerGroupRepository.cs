using DMSpro.OMS.MdmService.CustomerGroups;
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

namespace DMSpro.OMS.MdmService.CustomerGroups
{
    public partial class EfCoreCustomerGroupRepository : EfCoreRepository<MdmServiceDbContext, CustomerGroup, Guid>, ICustomerGroupRepository
    {
        public EfCoreCustomerGroupRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<CustomerGroup>> GetListAsync(
            string filterText = null,
            string code = null,
            string name = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            Type? groupBy = null,
            Status? status = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, name, active, effectiveDateMin, effectiveDateMax, groupBy, status);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerGroupConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            Type? groupBy = null,
            Status? status = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, code, name, active, effectiveDateMin, effectiveDateMax, groupBy, status);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CustomerGroup> ApplyFilter(
            IQueryable<CustomerGroup> query,
            string filterText,
            string code = null,
            string name = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            Type? groupBy = null,
            Status? status = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.Name.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(active.HasValue, e => e.Active == active)
                    .WhereIf(effectiveDateMin.HasValue, e => e.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(groupBy.HasValue, e => e.GroupBy == groupBy)
                    .WhereIf(status.HasValue, e => e.Status == status);
        }
    }
}