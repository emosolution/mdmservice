using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.ItemImages
{
    public interface IItemImageRepository : IRepository<ItemImage, Guid>
    {
        Task<ItemImageWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<ItemImageWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string description = null,
            string url = null,
            bool? active = null,
            int? displayOrderMin = null,
            int? displayOrderMax = null,
            Guid? itemId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<ItemImage>> GetListAsync(
                    string filterText = null,
                    string description = null,
                    string url = null,
                    bool? active = null,
                    int? displayOrderMin = null,
                    int? displayOrderMax = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string description = null,
            string url = null,
            bool? active = null,
            int? displayOrderMin = null,
            int? displayOrderMax = null,
            Guid? itemId = null,
            CancellationToken cancellationToken = default);
    }
}