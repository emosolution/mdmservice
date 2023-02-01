using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.SalesChannels
{
    public partial interface ISalesChannelRepository : IRepository<SalesChannel, Guid>
    {
        Task<List<SalesChannel>> GetListAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string description = null,
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
            string description = null,
            bool? active = null,
            CancellationToken cancellationToken = default);
    }
}