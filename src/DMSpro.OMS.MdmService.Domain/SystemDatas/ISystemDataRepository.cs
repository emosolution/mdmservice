using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.SystemDatas
{
    public interface ISystemDataRepository : IRepository<SystemData, Guid>
    {
        Task<List<SystemData>> GetListAsync(
            string filterText = null,
            string code = null,
            string valueCode = null,
            string valueName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string valueCode = null,
            string valueName = null,
            CancellationToken cancellationToken = default);
        Task<bool> CreateWithExcepAsync(List<SystemData> seedData);
    }
}
