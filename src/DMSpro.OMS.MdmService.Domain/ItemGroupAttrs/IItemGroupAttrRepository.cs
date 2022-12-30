using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.ItemGroupAttrs
{
    public interface IItemGroupAttrRepository : IRepository<ItemGroupAttr, Guid>
    {
        Task<ItemGroupAttrWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<ItemGroupAttrWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            bool? dummy = null,
            Guid? itemGroupId = null,
            Guid? attr0 = null,
            Guid? attr1 = null,
            Guid? attr2 = null,
            Guid? attr3 = null,
            Guid? attr4 = null,
            Guid? attr5 = null,
            Guid? attr6 = null,
            Guid? attr7 = null,
            Guid? attr8 = null,
            Guid? attr9 = null,
            Guid? attr10 = null,
            Guid? attr11 = null,
            Guid? attr12 = null,
            Guid? attr13 = null,
            Guid? attr14 = null,
            Guid? attr15 = null,
            Guid? attr16 = null,
            Guid? attr17 = null,
            Guid? attr18 = null,
            Guid? attr19 = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<ItemGroupAttr>> GetListAsync(
                    string filterText = null,
                    bool? dummy = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            bool? dummy = null,
            Guid? itemGroupId = null,
            Guid? attr0 = null,
            Guid? attr1 = null,
            Guid? attr2 = null,
            Guid? attr3 = null,
            Guid? attr4 = null,
            Guid? attr5 = null,
            Guid? attr6 = null,
            Guid? attr7 = null,
            Guid? attr8 = null,
            Guid? attr9 = null,
            Guid? attr10 = null,
            Guid? attr11 = null,
            Guid? attr12 = null,
            Guid? attr13 = null,
            Guid? attr14 = null,
            Guid? attr15 = null,
            Guid? attr16 = null,
            Guid? attr17 = null,
            Guid? attr18 = null,
            Guid? attr19 = null,
            CancellationToken cancellationToken = default);
    }
}