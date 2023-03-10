using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public partial interface INumberingConfigDetailRepository : IRepository<NumberingConfigDetail, Guid>
    {
        Task<NumberingConfigDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<List<NumberingConfigDetailWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string description = null,
            string prefix = null,
            int? paddingZeroNumberMin = null,
            int? paddingZeroNumberMax = null,
            string suffix = null,
            bool? active = null,
            int? currentNumberMin = null,
            int? currentNumberMax = null,
            Guid? numberingConfigId = null,
            Guid? companyId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<NumberingConfigDetail>> GetListAsync(
                    string filterText = null,
                    string description = null,
                    string prefix = null,
                    int? paddingZeroNumberMin = null,
                    int? paddingZeroNumberMax = null,
                    string suffix = null,
                    bool? active = null,
                    int? currentNumberMin = null,
                    int? currentNumberMax = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string description = null,
            string prefix = null,
            int? paddingZeroNumberMin = null,
            int? paddingZeroNumberMax = null,
            string suffix = null,
            bool? active = null,
            int? currentNumberMin = null,
            int? currentNumberMax = null,
            Guid? numberingConfigId = null,
            Guid? companyId = null,
            CancellationToken cancellationToken = default);
    }
}