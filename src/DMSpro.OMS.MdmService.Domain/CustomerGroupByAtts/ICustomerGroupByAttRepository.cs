using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
    public partial interface ICustomerGroupByAttRepository : IRepository<CustomerGroupByAtt, Guid>
    {
        Task<CustomerGroupByAttWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<CustomerGroupByAttWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string valueCode = null,
            string valueName = null,
            Guid? customerGroupId = null,
            Guid? cusAttributeValueId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<CustomerGroupByAtt>> GetListAsync(
                    string filterText = null,
                    string valueCode = null,
                    string valueName = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string valueCode = null,
            string valueName = null,
            Guid? customerGroupId = null,
            Guid? cusAttributeValueId = null,
            CancellationToken cancellationToken = default);
    }
}