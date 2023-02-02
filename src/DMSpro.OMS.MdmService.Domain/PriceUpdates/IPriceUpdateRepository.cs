using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public partial interface IPriceUpdateRepository : IRepository<PriceUpdate, Guid>
    {
        Task<PriceUpdateWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<PriceUpdateWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string code = null,
            string description = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            PriceUpdateStatus? status = null,
            DateTime? updateStatusDateMin = null,
            DateTime? updateStatusDateMax = null,
            Guid? priceListId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<PriceUpdate>> GetListAsync(
                    string filterText = null,
                    string code = null,
                    string description = null,
                    DateTime? effectiveDateMin = null,
                    DateTime? effectiveDateMax = null,
                    PriceUpdateStatus? status = null,
                    DateTime? updateStatusDateMin = null,
                    DateTime? updateStatusDateMax = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string description = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            PriceUpdateStatus? status = null,
            DateTime? updateStatusDateMin = null,
            DateTime? updateStatusDateMax = null,
            Guid? priceListId = null,
            CancellationToken cancellationToken = default);
    }
}