using DMSpro.OMS.MdmService.SystemConfigs;
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

namespace DMSpro.OMS.MdmService.SystemConfigs
{
    public partial class EfCoreSystemConfigRepository : EfCoreRepository<MdmServiceDbContext, SystemConfig, Guid>, ISystemConfigRepository
    {
        public EfCoreSystemConfigRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<SystemConfig>> GetListAsync(
            string filterText = null,
            string code = null,
            string description = null,
            string value = null,
            string defaultValue = null,
            bool? editableByTenant = null,
            ControlType? controlType = null,
            string dataSource = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, description, value, defaultValue, editableByTenant, controlType, dataSource);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? SystemConfigConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string description = null,
            string value = null,
            string defaultValue = null,
            bool? editableByTenant = null,
            ControlType? controlType = null,
            string dataSource = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, code, description, value, defaultValue, editableByTenant, controlType, dataSource);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<SystemConfig> ApplyFilter(
            IQueryable<SystemConfig> query,
            string filterText,
            string code = null,
            string description = null,
            string value = null,
            string defaultValue = null,
            bool? editableByTenant = null,
            ControlType? controlType = null,
            string dataSource = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.Description.Contains(filterText) || e.Value.Contains(filterText) || e.DefaultValue.Contains(filterText) || e.DataSource.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(!string.IsNullOrWhiteSpace(value), e => e.Value.Contains(value))
                    .WhereIf(!string.IsNullOrWhiteSpace(defaultValue), e => e.DefaultValue.Contains(defaultValue))
                    .WhereIf(editableByTenant.HasValue, e => e.EditableByTenant == editableByTenant)
                    .WhereIf(controlType.HasValue, e => e.ControlType == controlType)
                    .WhereIf(!string.IsNullOrWhiteSpace(dataSource), e => e.DataSource.Contains(dataSource));
        }
    }
}