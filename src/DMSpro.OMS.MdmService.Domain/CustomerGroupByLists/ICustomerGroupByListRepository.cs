using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
    public interface ICustomerGroupByListRepository : IRepository<CustomerGroupByList, Guid>
    {
        Task<CustomerGroupByListWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<CustomerGroupByListWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            bool? active = null,
            Guid? customerGroupId = null,
            Guid? customerId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<CustomerGroupByList>> GetListAsync(
                    string filterText = null,
                    bool? active = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            bool? active = null,
            Guid? customerGroupId = null,
            Guid? customerId = null,
            CancellationToken cancellationToken = default);
    }
}