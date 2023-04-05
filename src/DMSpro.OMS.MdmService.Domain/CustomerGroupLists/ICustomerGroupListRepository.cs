using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public interface ICustomerGroupListRepository : IRepository<CustomerGroupList, Guid>
    {
        Task<CustomerGroupListWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<CustomerGroupListWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string description = null,
            bool? active = null,
            Guid? customerId = null,
            Guid? customerGroupId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<CustomerGroupList>> GetListAsync(
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
            Guid? customerId = null,
            Guid? customerGroupId = null,
            CancellationToken cancellationToken = default);
    }
}