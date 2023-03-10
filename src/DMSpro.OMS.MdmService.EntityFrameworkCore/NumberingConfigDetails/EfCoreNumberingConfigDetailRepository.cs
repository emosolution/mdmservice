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

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public partial class EfCoreNumberingConfigDetailRepository : 
        EfCoreRepository<MdmServiceDbContext, NumberingConfigDetail, Guid>, 
        INumberingConfigDetailRepository
    {
        public EfCoreNumberingConfigDetailRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<NumberingConfigDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(numberingConfigDetail => new NumberingConfigDetailWithNavigationProperties
                {
                    NumberingConfigDetail = numberingConfigDetail,
                    NumberingConfig = dbContext.NumberingConfigs.FirstOrDefault(c => c.Id == numberingConfigDetail.NumberingConfigId),
                    Company = dbContext.Companies.FirstOrDefault(c => c.Id == numberingConfigDetail.CompanyId)
                }).FirstOrDefault();
        }

        public async Task<List<NumberingConfigDetailWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string description = null,
            string prefix = null,
            int? paddingZeroNumberMin = null,
            int? paddingZeroNumberMax = null,
            string suffix = null,
            bool? active = null,
            int? currentNumberMin = null,
            int? currentNumberMax = null,
            Guid? numberingConfigId = null,
            Guid? companyId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, description, prefix, paddingZeroNumberMin, paddingZeroNumberMax, suffix, active, currentNumberMin, currentNumberMax, numberingConfigId, companyId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? NumberingConfigDetailConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<NumberingConfigDetailWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from numberingConfigDetail in (await GetDbSetAsync())
                   join numberingConfig in (await GetDbContextAsync()).NumberingConfigs on numberingConfigDetail.NumberingConfigId equals numberingConfig.Id into numberingConfigs
                   from numberingConfig in numberingConfigs.DefaultIfEmpty()
                   join company in (await GetDbContextAsync()).Companies on numberingConfigDetail.CompanyId equals company.Id into companies
                   from company in companies.DefaultIfEmpty()

                   select new NumberingConfigDetailWithNavigationProperties
                   {
                       NumberingConfigDetail = numberingConfigDetail,
                       NumberingConfig = numberingConfig,
                       Company = company
                   };
        }

        protected virtual IQueryable<NumberingConfigDetailWithNavigationProperties> ApplyFilter(
            IQueryable<NumberingConfigDetailWithNavigationProperties> query,
            string filterText,
            string description = null,
            string prefix = null,
            int? paddingZeroNumberMin = null,
            int? paddingZeroNumberMax = null,
            string suffix = null,
            bool? active = null,
            int? currentNumberMin = null,
            int? currentNumberMax = null,
            Guid? numberingConfigId = null,
            Guid? companyId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.NumberingConfigDetail.Description.Contains(filterText) || e.NumberingConfigDetail.Prefix.Contains(filterText) || e.NumberingConfigDetail.Suffix.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.NumberingConfigDetail.Description.Contains(description))
                    .WhereIf(!string.IsNullOrWhiteSpace(prefix), e => e.NumberingConfigDetail.Prefix.Contains(prefix))
                    .WhereIf(paddingZeroNumberMin.HasValue, e => e.NumberingConfigDetail.PaddingZeroNumber >= paddingZeroNumberMin.Value)
                    .WhereIf(paddingZeroNumberMax.HasValue, e => e.NumberingConfigDetail.PaddingZeroNumber <= paddingZeroNumberMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(suffix), e => e.NumberingConfigDetail.Suffix.Contains(suffix))
                    .WhereIf(active.HasValue, e => e.NumberingConfigDetail.Active == active)
                    .WhereIf(currentNumberMin.HasValue, e => e.NumberingConfigDetail.CurrentNumber >= currentNumberMin.Value)
                    .WhereIf(currentNumberMax.HasValue, e => e.NumberingConfigDetail.CurrentNumber <= currentNumberMax.Value)
                    .WhereIf(numberingConfigId != null && numberingConfigId != Guid.Empty, e => e.NumberingConfig != null && e.NumberingConfig.Id == numberingConfigId)
                    .WhereIf(companyId != null && companyId != Guid.Empty, e => e.Company != null && e.Company.Id == companyId);
        }

        public async Task<List<NumberingConfigDetail>> GetListAsync(
            string filterText = null,
            string description = null,
            string prefix = null,
            int? paddingZeroNumberMin = null,
            int? paddingZeroNumberMax = null,
            string suffix = null,
            bool? active = null,
            int? currentNumberMin = null,
            int? currentNumberMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, description, prefix, paddingZeroNumberMin, paddingZeroNumberMax, suffix, active, currentNumberMin, currentNumberMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? NumberingConfigDetailConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string description = null,
            string prefix = null,
            int? paddingZeroNumberMin = null,
            int? paddingZeroNumberMax = null,
            string suffix = null,
            bool? active = null,
            int? currentNumberMin = null,
            int? currentNumberMax = null,
            Guid? numberingConfigId = null,
            Guid? companyId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, description, prefix, paddingZeroNumberMin, paddingZeroNumberMax, suffix, active, currentNumberMin, currentNumberMax, numberingConfigId, companyId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<NumberingConfigDetail> ApplyFilter(
            IQueryable<NumberingConfigDetail> query,
            string filterText,
            string description = null,
            string prefix = null,
            int? paddingZeroNumberMin = null,
            int? paddingZeroNumberMax = null,
            string suffix = null,
            bool? active = null,
            int? currentNumberMin = null,
            int? currentNumberMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Description.Contains(filterText) || e.Prefix.Contains(filterText) || e.Suffix.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(!string.IsNullOrWhiteSpace(prefix), e => e.Prefix.Contains(prefix))
                    .WhereIf(paddingZeroNumberMin.HasValue, e => e.PaddingZeroNumber >= paddingZeroNumberMin.Value)
                    .WhereIf(paddingZeroNumberMax.HasValue, e => e.PaddingZeroNumber <= paddingZeroNumberMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(suffix), e => e.Suffix.Contains(suffix))
                    .WhereIf(active.HasValue, e => e.Active == active)
                    .WhereIf(currentNumberMin.HasValue, e => e.CurrentNumber >= currentNumberMin.Value)
                    .WhereIf(currentNumberMax.HasValue, e => e.CurrentNumber <= currentNumberMax.Value);
        }
    }
}