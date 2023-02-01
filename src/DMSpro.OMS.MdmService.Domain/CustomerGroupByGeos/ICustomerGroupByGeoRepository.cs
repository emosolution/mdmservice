using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
    public partial interface ICustomerGroupByGeoRepository : IRepository<CustomerGroupByGeo, Guid>
    {
        Task<CustomerGroupByGeoWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<CustomerGroupByGeoWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            Guid? customerGroupId = null,
            Guid? geoMasterId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<CustomerGroupByGeo>> GetListAsync(
                    string filterText = null,
                    bool? active = null,
                    DateTime? effectiveDateMin = null,
                    DateTime? effectiveDateMax = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            Guid? customerGroupId = null,
            Guid? geoMasterId = null,
            CancellationToken cancellationToken = default);
    }
}