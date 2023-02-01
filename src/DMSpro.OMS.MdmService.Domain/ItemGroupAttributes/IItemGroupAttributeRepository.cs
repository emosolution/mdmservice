using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.ItemGroupAttributes
{
    public partial interface IItemGroupAttributeRepository : IRepository<ItemGroupAttribute, Guid>
    {
        Task<ItemGroupAttributeWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<ItemGroupAttributeWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string dummy = null,
            Guid? itemGroupId = null,
            Guid? attr0Id = null,
            Guid? attr1Id = null,
            Guid? attr2Id = null,
            Guid? attr3Id = null,
            Guid? attr4Id = null,
            Guid? attr6Id = null,
            Guid? attr7Id = null,
            Guid? attr8Id = null,
            Guid? attr9Id = null,
            Guid? attr10Id = null,
            Guid? attr11Id = null,
            Guid? attr12Id = null,
            Guid? attr13Id = null,
            Guid? attr14Id = null,
            Guid? attr15Id = null,
            Guid? attr16Id = null,
            Guid? attr17Id = null,
            Guid? attr18Id = null,
            Guid? attr19Id = null,
            Guid? attr5Id = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<ItemGroupAttribute>> GetListAsync(
                    string filterText = null,
                    string dummy = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string dummy = null,
            Guid? itemGroupId = null,
            Guid? attr0Id = null,
            Guid? attr1Id = null,
            Guid? attr2Id = null,
            Guid? attr3Id = null,
            Guid? attr4Id = null,
            Guid? attr6Id = null,
            Guid? attr7Id = null,
            Guid? attr8Id = null,
            Guid? attr9Id = null,
            Guid? attr10Id = null,
            Guid? attr11Id = null,
            Guid? attr12Id = null,
            Guid? attr13Id = null,
            Guid? attr14Id = null,
            Guid? attr15Id = null,
            Guid? attr16Id = null,
            Guid? attr17Id = null,
            Guid? attr18Id = null,
            Guid? attr19Id = null,
            Guid? attr5Id = null,
            CancellationToken cancellationToken = default);
    }
}