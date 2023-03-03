using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.DimensionMeasurements
{
    public partial interface IDimensionMeasurementRepository : IRepository<DimensionMeasurement, Guid>
    {
        Task<List<DimensionMeasurement>> GetListAsync(
            string filterText = null,
            string code = null,
            string name = null,
            decimal? valueMin = null,
            decimal? valueMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            decimal? valueMin = null,
            decimal? valueMax = null,
            CancellationToken cancellationToken = default);
    }
}