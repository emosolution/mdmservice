using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    public interface ICustomerAttributeValueRepository : IRepository<CustomerAttributeValue, Guid>
    {
        Task<CustomerAttributeValueWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<CustomerAttributeValueWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string code = null,
            string attrValName = null,
            Guid? customerAttributeId = null,
            Guid? parentId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<CustomerAttributeValue>> GetListAsync(
                    string filterText = null,
                    string code = null,
                    string attrValName = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string attrValName = null,
            Guid? customerAttributeId = null,
            Guid? parentId = null,
            CancellationToken cancellationToken = default);
    }
}