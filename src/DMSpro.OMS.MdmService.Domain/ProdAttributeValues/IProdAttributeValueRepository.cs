using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.ProdAttributeValues
{
    public interface IProdAttributeValueRepository : IRepository<ProdAttributeValue, Guid>
    {
        Task<ProdAttributeValueWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<ProdAttributeValueWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string attrValName = null,
            Guid? prodAttributeId = null,
            Guid? parentProdAttributeValueId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<ProdAttributeValue>> GetListAsync(
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
            Guid? prodAttributeId = null,
            Guid? parentProdAttributeValueId = null,
            CancellationToken cancellationToken = default);
    }
}