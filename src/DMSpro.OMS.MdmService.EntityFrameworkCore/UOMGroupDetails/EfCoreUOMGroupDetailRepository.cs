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

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
    public partial class EfCoreUOMGroupDetailRepository : EfCoreRepository<MdmServiceDbContext, UOMGroupDetail, Guid>, IUOMGroupDetailRepository
    {
        public EfCoreUOMGroupDetailRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<UOMGroupDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(uOMGroupDetail => new UOMGroupDetailWithNavigationProperties
                {
                    UOMGroupDetail = uOMGroupDetail,
                    UOMGroup = dbContext.UOMGroups.FirstOrDefault(c => c.Id == uOMGroupDetail.UOMGroupId),
                    AltUOM = dbContext.UOMs.FirstOrDefault(c => c.Id == uOMGroupDetail.AltUOMId),
                    BaseUOM = dbContext.UOMs.FirstOrDefault(c => c.Id == uOMGroupDetail.BaseUOMId)
                }).FirstOrDefault();
        }

        public async Task<List<UOMGroupDetailWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            uint? altQtyMin = null,
            uint? altQtyMax = null,
            uint? baseQtyMin = null,
            uint? baseQtyMax = null,
            bool? active = null,
            Guid? uOMGroupId = null,
            Guid? altUOMId = null,
            Guid? baseUOMId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, altQtyMin, altQtyMax, baseQtyMin, baseQtyMax, active, uOMGroupId, altUOMId, baseUOMId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? UOMGroupDetailConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<UOMGroupDetailWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from uOMGroupDetail in (await GetDbSetAsync())
                   join uOMGroup in (await GetDbContextAsync()).UOMGroups on uOMGroupDetail.UOMGroupId equals uOMGroup.Id into uOMGroups
                   from uOMGroup in uOMGroups.DefaultIfEmpty()
                   join altUOM in (await GetDbContextAsync()).UOMs on uOMGroupDetail.AltUOMId equals altUOM.Id into uOMs
                   from altUOM in uOMs.DefaultIfEmpty()
                   join baseUOM in (await GetDbContextAsync()).UOMs on uOMGroupDetail.BaseUOMId equals baseUOM.Id into uOMs1
                   from baseUOM in uOMs1.DefaultIfEmpty()

                   select new UOMGroupDetailWithNavigationProperties
                   {
                       UOMGroupDetail = uOMGroupDetail,
                       UOMGroup = uOMGroup,
                       AltUOM = altUOM,
                       BaseUOM = baseUOM
                   };
        }

        public virtual async Task<IQueryable<UOMGroupDetailWithNavigationProperties>> GetQueryAbleForNavigationPropertiesAsync()
        {
            return from uOMGroupDetail in (await GetDbSetAsync())
                   join uOMGroup in (await GetDbContextAsync()).UOMGroups on uOMGroupDetail.UOMGroupId equals uOMGroup.Id into uOMGroups
                   from uOMGroup in uOMGroups.DefaultIfEmpty()
                   join altUOM in (await GetDbContextAsync()).UOMs on uOMGroupDetail.AltUOMId equals altUOM.Id into uOMs
                   from altUOM in uOMs.DefaultIfEmpty()
                   join baseUOM in (await GetDbContextAsync()).UOMs on uOMGroupDetail.BaseUOMId equals baseUOM.Id into uOMs1
                   from baseUOM in uOMs1.DefaultIfEmpty()

                   select new UOMGroupDetailWithNavigationProperties
                   {
                       UOMGroupDetail = uOMGroupDetail,
                       UOMGroup = uOMGroup,
                       AltUOM = altUOM,
                       BaseUOM = baseUOM
                   };
        }

        protected virtual IQueryable<UOMGroupDetailWithNavigationProperties> ApplyFilter(
            IQueryable<UOMGroupDetailWithNavigationProperties> query,
            string filterText,
            uint? altQtyMin = null,
            uint? altQtyMax = null,
            uint? baseQtyMin = null,
            uint? baseQtyMax = null,
            bool? active = null,
            Guid? uOMGroupId = null,
            Guid? altUOMId = null,
            Guid? baseUOMId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(altQtyMin.HasValue, e => e.UOMGroupDetail.AltQty >= altQtyMin.Value)
                    .WhereIf(altQtyMax.HasValue, e => e.UOMGroupDetail.AltQty <= altQtyMax.Value)
                    .WhereIf(baseQtyMin.HasValue, e => e.UOMGroupDetail.BaseQty >= baseQtyMin.Value)
                    .WhereIf(baseQtyMax.HasValue, e => e.UOMGroupDetail.BaseQty <= baseQtyMax.Value)
                    .WhereIf(active.HasValue, e => e.UOMGroupDetail.Active == active)
                    .WhereIf(uOMGroupId != null && uOMGroupId != Guid.Empty, e => e.UOMGroup != null && e.UOMGroup.Id == uOMGroupId)
                    .WhereIf(altUOMId != null && altUOMId != Guid.Empty, e => e.AltUOM != null && e.AltUOM.Id == altUOMId)
                    .WhereIf(baseUOMId != null && baseUOMId != Guid.Empty, e => e.BaseUOM != null && e.BaseUOM.Id == baseUOMId);
        }

        public async Task<List<UOMGroupDetail>> GetListAsync(
            string filterText = null,
            uint? altQtyMin = null,
            uint? altQtyMax = null,
            uint? baseQtyMin = null,
            uint? baseQtyMax = null,
            bool? active = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, altQtyMin, altQtyMax, baseQtyMin, baseQtyMax, active);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? UOMGroupDetailConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            uint? altQtyMin = null,
            uint? altQtyMax = null,
            uint? baseQtyMin = null,
            uint? baseQtyMax = null,
            bool? active = null,
            Guid? uOMGroupId = null,
            Guid? altUOMId = null,
            Guid? baseUOMId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, altQtyMin, altQtyMax, baseQtyMin, baseQtyMax, active, uOMGroupId, altUOMId, baseUOMId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<UOMGroupDetail> ApplyFilter(
            IQueryable<UOMGroupDetail> query,
            string filterText,
            uint? altQtyMin = null,
            uint? altQtyMax = null,
            uint? baseQtyMin = null,
            uint? baseQtyMax = null,
            bool? active = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(altQtyMin.HasValue, e => e.AltQty >= altQtyMin.Value)
                    .WhereIf(altQtyMax.HasValue, e => e.AltQty <= altQtyMax.Value)
                    .WhereIf(baseQtyMin.HasValue, e => e.BaseQty >= baseQtyMin.Value)
                    .WhereIf(baseQtyMax.HasValue, e => e.BaseQty <= baseQtyMax.Value)
                    .WhereIf(active.HasValue, e => e.Active == active);
        }
    }
}