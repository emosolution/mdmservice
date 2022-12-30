using DMSpro.OMS.MdmService.ItemMasters;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.ItemGroups
{
    public interface IItemGroupRepository : IRepository<ItemGroup, Guid>
    {
        Task<List<ItemGroup>> GetListAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string description = null,
            GroupType? type = null,
            Status? status = null,
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
            GroupType? type = null,
            Status? status = null,
            CancellationToken cancellationToken = default);
    }
}