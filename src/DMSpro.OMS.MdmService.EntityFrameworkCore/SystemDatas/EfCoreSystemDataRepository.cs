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

namespace DMSpro.OMS.MdmService.SystemDatas
{
    public class EfCoreSystemDataRepository : EfCoreRepository<MdmServiceDbContext, SystemData, Guid>, ISystemDataRepository
    {
        public EfCoreSystemDataRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<SystemData>> GetListAsync(
            string filterText = null,
            string code = null,
            string valueCode = null,
            string valueName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, valueCode, valueName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? SystemDataConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string valueCode = null,
            string valueName = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, code, valueCode, valueName);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<SystemData> ApplyFilter(
            IQueryable<SystemData> query,
            string filterText,
            string code = null,
            string valueCode = null,
            string valueName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.ValueCode.Contains(filterText) || e.ValueName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(valueCode), e => e.ValueCode.Contains(valueCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(valueName), e => e.ValueName.Contains(valueName));
        }
        public async Task<bool> CreateWithExcepAsync(List<SystemData> seedData)
        {
            await this.InsertManyAsync(seedData);
            return true;
        }
    }
}
