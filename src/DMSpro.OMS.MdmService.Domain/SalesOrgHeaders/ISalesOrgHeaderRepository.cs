using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
    public interface ISalesOrgHeaderRepository : IRepository<SalesOrgHeader, Guid>
    {
        Task<List<SalesOrgHeader>> GetListAsync(
            string filterText = null,
            string code = null,
            string name = null,
            bool? active = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            bool? active = null,
            CancellationToken cancellationToken = default);
    }
}