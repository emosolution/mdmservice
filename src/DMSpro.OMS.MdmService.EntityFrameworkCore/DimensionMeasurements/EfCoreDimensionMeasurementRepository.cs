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

namespace DMSpro.OMS.MdmService.DimensionMeasurements
{
    public class EfCoreDimensionMeasurementRepository : EfCoreRepository<MdmServiceDbContext, DimensionMeasurement, Guid>, IDimensionMeasurementRepository
    {
        public EfCoreDimensionMeasurementRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<DimensionMeasurement>> GetListAsync(
            string filterText = null,
            string code = null,
            string name = null,
            uint? valueMin = null,
            uint? valueMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, name, valueMin, valueMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DimensionMeasurementConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            uint? valueMin = null,
            uint? valueMax = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, code, name, valueMin, valueMax);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<DimensionMeasurement> ApplyFilter(
            IQueryable<DimensionMeasurement> query,
            string filterText,
            string code = null,
            string name = null,
            uint? valueMin = null,
            uint? valueMax = null)
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