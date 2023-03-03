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

namespace DMSpro.OMS.MdmService.WeightMeasurements
{
    public partial class EfCoreWeightMeasurementRepository : EfCoreRepository<MdmServiceDbContext, WeightMeasurement, Guid>, IWeightMeasurementRepository
    {
        public EfCoreWeightMeasurementRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<WeightMeasurement>> GetListAsync(
            string filterText = null,
            string code = null,
            string name = null,
            decimal? valueMin = null,
            decimal? valueMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, name, valueMin, valueMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? WeightMeasurementConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            decimal? valueMin = null,
            decimal? valueMax = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, code, name, valueMin, valueMax);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<WeightMeasurement> ApplyFilter(
            IQueryable<WeightMeasurement> query,
            string filterText,
            string code = null,
            string name = null,
            decimal? valueMin = null,
            decimal? valueMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.Name.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(valueMin.HasValue, e => e.Value >= valueMin.Value)
                    .WhereIf(valueMax.HasValue, e => e.Value <= valueMax.Value);
        }
    }
}