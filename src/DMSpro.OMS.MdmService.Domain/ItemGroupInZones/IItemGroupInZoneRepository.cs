using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.ItemGroupInZones
{
    public interface IItemGroupInZoneRepository : IRepository<ItemGroupInZone, Guid>
    {
        Task<ItemGroupInZoneWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<ItemGroupInZoneWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            bool? active = null,
            string description = null,
            Guid? sellingZoneId = null,
            Guid? itemGroupId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<ItemGroupInZone>> GetListAsync(
                    string filterText = null,
                    DateTime? effectiveDateMin = null,
                    DateTime? effectiveDateMax = null,
                    DateTime? endDateMin = null,
                    DateTime? endDateMax = null,
                    bool? active = null,
                    string description = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            bool? active = null,
            string description = null,
            Guid? sellingZoneId = null,
            Guid? itemGroupId = null,
            CancellationToken cancellationToken = default);
    }
}