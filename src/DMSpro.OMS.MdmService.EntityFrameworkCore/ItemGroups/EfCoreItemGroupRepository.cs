using DMSpro.OMS.MdmService.ItemGroups;
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

namespace DMSpro.OMS.MdmService.ItemGroups
{
    public partial class EfCoreItemGroupRepository : EfCoreRepository<MdmServiceDbContext, ItemGroup, Guid>, IItemGroupRepository
    {
        public EfCoreItemGroupRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<ItemGroup>> GetListAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string description = null,
            GroupType? type = null,
            GroupStatus? status = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, name, description, type, status);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ItemGroupConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string description = null,
            GroupType? type = null,
            GroupStatus? status = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, code, name, description, type, status);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<ItemGroup> ApplyFilter(
            IQueryable<ItemGroup> query,
            string filterText,
            string code = null,
            string name = null,
            string description = null,
            GroupType? type = null,
            GroupStatus? status = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.Name.Contains(filterText) || e.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(type.HasValue, e => e.Type == type)
                    .WhereIf(status.HasValue, e => e.Status == status);
        }
    }
}