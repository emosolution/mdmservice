using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.VATs
{
    public partial interface IVATRepository : IRepository<VAT, Guid>
    {
        Task<List<VAT>> GetListAsync(
            string filterText = null,
            string code = null,
            string name = null,
            uint? rateMin = null,
            uint? rateMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            uint? rateMin = null,
            uint? rateMax = null,
            CancellationToken cancellationToken = default);
    }
}