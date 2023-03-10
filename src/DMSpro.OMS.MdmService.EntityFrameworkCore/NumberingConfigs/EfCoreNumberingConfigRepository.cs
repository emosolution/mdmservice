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

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public partial class EfCoreNumberingConfigRepository : EfCoreRepository<MdmServiceDbContext, NumberingConfig, Guid>, INumberingConfigRepository
    {
        public EfCoreNumberingConfigRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<NumberingConfigWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(numberingConfig => new NumberingConfigWithNavigationProperties
                {
                    NumberingConfig = numberingConfig,
                    SystemData = dbContext.SystemDatas.FirstOrDefault(c => c.Id == numberingConfig.SystemDataId)
                }).FirstOrDefault();
        }

        public async Task<List<NumberingConfigWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string prefix = null,
            string suffix = null,
            int? paddingZeroNumberMin = null,
            int? paddingZeroNumberMax = null,
            string description = null,
            Guid? systemDataId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, prefix, suffix, paddingZeroNumberMin, paddingZeroNumberMax, description, systemDataId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? NumberingConfigConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<NumberingConfigWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from numberingConfig in (await GetDbSetAsync())
                   join systemData in (await GetDbContextAsync()).SystemDatas on numberingConfig.SystemDataId equals systemData.Id into systemDatas
                   from systemData in systemDatas.DefaultIfEmpty()

                   select new NumberingConfigWithNavigationProperties
                   {
                       NumberingConfig = numberingConfig,
                       SystemData = systemData
                   };
        }

        protected virtual IQueryable<NumberingConfigWithNavigationProperties> ApplyFilter(
            IQueryable<NumberingConfigWithNavigationProperties> query,
            string filterText,
            string prefix = null,
            string suffix = null,
            int? paddingZeroNumberMin = null,
            int? paddingZeroNumberMax = null,
            string description = null,
            Guid? systemDataId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.NumberingConfig.Prefix.Contains(filterText) || e.NumberingConfig.Suffix.Contains(filterText) || e.NumberingConfig.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(prefix), e => e.NumberingConfig.Prefix.Contains(prefix))
                    .WhereIf(!string.IsNullOrWhiteSpace(suffix), e => e.NumberingConfig.Suffix.Contains(suffix))
                    .WhereIf(paddingZeroNumberMin.HasValue, e => e.NumberingConfig.PaddingZeroNumber >= paddingZeroNumberMin.Value)
                    .WhereIf(paddingZeroNumberMax.HasValue, e => e.NumberingConfig.PaddingZeroNumber <= paddingZeroNumberMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.NumberingConfig.Description.Contains(description))
                    .WhereIf(systemDataId != null && systemDataId != Guid.Empty, e => e.SystemData != null && e.SystemData.Id == systemDataId);
        }

        public async Task<List<NumberingConfig>> GetListAsync(
            string filterText = null,
            string prefix = null,
            string suffix = null,
            int? paddingZeroNumberMin = null,
            int? paddingZeroNumberMax = null,
            string description = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, prefix, suffix, paddingZeroNumberMin, paddingZeroNumberMax, description);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? NumberingConfigConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string prefix = null,
            string suffix = null,
            int? paddingZeroNumberMin = null,
            int? paddingZeroNumberMax = null,
            string description = null,
            Guid? systemDataId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, prefix, suffix, paddingZeroNumberMin, paddingZeroNumberMax, description, systemDataId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<NumberingConfig> ApplyFilter(
            IQueryable<NumberingConfig> query,
            string filterText,
            string prefix = null,
            string suffix = null,
            int? paddingZeroNumberMin = null,
            int? paddingZeroNumberMax = null,
            string description = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Prefix.Contains(filterText) || e.Suffix.Contains(filterText) || e.Description.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(prefix), e => e.Prefix.Contains(prefix))
                    .WhereIf(!string.IsNullOrWhiteSpace(suffix), e => e.Suffix.Contains(suffix))
                    .WhereIf(paddingZeroNumberMin.HasValue, e => e.PaddingZeroNumber >= paddingZeroNumberMin.Value)
                    .WhereIf(paddingZeroNumberMax.HasValue, e => e.PaddingZeroNumber <= paddingZeroNumberMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description));
        }
    }
}