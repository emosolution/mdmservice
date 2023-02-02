using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public partial interface IPricelistAssignmentRepository : IRepository<PricelistAssignment, Guid>
    {
        Task<PricelistAssignmentWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<PricelistAssignmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string description = null,
            Guid? priceListId = null,
            Guid? customerGroupId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<PricelistAssignment>> GetListAsync(
                    string filterText = null,
                    string description = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string description = null,
            Guid? priceListId = null,
            Guid? customerGroupId = null,
            CancellationToken cancellationToken = default);
    }
}