using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    public interface IItemGroupListRepository : IRepository<ItemGroupList, Guid>
    {
        Task<ItemGroupListWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<ItemGroupListWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            int? rateMin = null,
            int? rateMax = null,
            Guid? itemGroupId = null,
            Guid? itemId = null,
            Guid? uomId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<ItemGroupList>> GetListAsync(
                    string filterText = null,
                    int? rateMin = null,
                    int? rateMax = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            int? rateMin = null,
            int? rateMax = null,
            Guid? itemGroupId = null,
            Guid? itemId = null,
            Guid? uomId = null,
            CancellationToken cancellationToken = default);
    }
}