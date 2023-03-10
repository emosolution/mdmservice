using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public partial interface INumberingConfigRepository : IRepository<NumberingConfig, Guid>
    {
        Task<NumberingConfigWithNavigationProperties> GetWithNavigationPropertiesAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<List<NumberingConfigWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string prefix = null,
            string suffix = null,
            int? paddingZeroNumberMin = null,
            int? paddingZeroNumberMax = null,
            string description = null,
            Guid? systemDataId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<NumberingConfig>> GetListAsync(
                    string filterText = null,
                    string prefix = null,
                    string suffix = null,
                    int? paddingZeroNumberMin = null,
                    int? paddingZeroNumberMax = null,
                    string description = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string prefix = null,
            string suffix = null,
            int? paddingZeroNumberMin = null,
            int? paddingZeroNumberMax = null,
            string description = null,
            Guid? systemDataId = null,
            CancellationToken cancellationToken = default);
    }
}