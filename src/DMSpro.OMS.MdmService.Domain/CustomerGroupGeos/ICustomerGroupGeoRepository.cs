using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public interface ICustomerGroupGeoRepository : IRepository<CustomerGroupGeo, Guid>
    {
        Task<CustomerGroupGeoWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<CustomerGroupGeoWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string description = null,
            bool? active = null,
            Guid? customerGroupId = null,
            Guid? geoMaster0Id = null,
            Guid? geoMaster1Id = null,
            Guid? geoMaster2Id = null,
            Guid? geoMaster3Id = null,
            Guid? geoMaster4Id = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<CustomerGroupGeo>> GetListAsync(
                    string filterText = null,
                    string description = null,
                    bool? active = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string description = null,
            bool? active = null,
            Guid? customerGroupId = null,
            Guid? geoMaster0Id = null,
            Guid? geoMaster1Id = null,
            Guid? geoMaster2Id = null,
            Guid? geoMaster3Id = null,
            Guid? geoMaster4Id = null,
            CancellationToken cancellationToken = default);
    }
}