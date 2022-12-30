using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.UOMGroups
{
    public interface IUOMGroupRepository : IRepository<UOMGroup, Guid>
    {
        Task<List<UOMGroup>> GetListAsync(
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