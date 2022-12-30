using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CusAttributeValues
{
    public interface ICusAttributeValueRepository : IRepository<CusAttributeValue, Guid>
    {
        Task<CusAttributeValueWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<CusAttributeValueWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string attrValName = null,
            Guid? customerAttributeId = null,
            Guid? parentCusAttributeValueId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<CusAttributeValue>> GetListAsync(
                    string filterText = null,
                    string attrValName = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string attrValName = null,
            Guid? customerAttributeId = null,
            Guid? parentCusAttributeValueId = null,
            CancellationToken cancellationToken = default);
    }
}