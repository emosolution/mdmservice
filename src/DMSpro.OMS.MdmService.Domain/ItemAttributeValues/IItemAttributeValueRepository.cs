using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public interface IItemAttributeValueRepository : IRepository<ItemAttributeValue, Guid>
    {
        Task<ItemAttributeValueWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<ItemAttributeValueWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string attrValName = null,
            Guid? itemAttributeId = null,
            Guid? parentId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<ItemAttributeValue>> GetListAsync(
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
            Guid? itemAttributeId = null,
            Guid? parentId = null,
            CancellationToken cancellationToken = default);
    }
}