using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.Currencies
{
    public partial interface ICurrencyRepository : IRepository<Currency, Guid>
    {
        Task<List<Currency>> GetListAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            CancellationToken cancellationToken = default);
    }
}