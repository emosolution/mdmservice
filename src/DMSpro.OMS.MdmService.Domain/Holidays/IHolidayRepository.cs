using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.Holidays
{
    public partial interface IHolidayRepository : IRepository<Holiday, Guid>
    {
        Task<List<Holiday>> GetListAsync(
            string filterText = null,
            int? yearMin = null,
            int? yearMax = null,
            string description = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            int? yearMin = null,
            int? yearMax = null,
            string description = null,
            CancellationToken cancellationToken = default);
    }
}