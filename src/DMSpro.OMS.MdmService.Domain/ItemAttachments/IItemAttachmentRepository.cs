using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public partial interface IItemAttachmentRepository : IRepository<ItemAttachment, Guid>
    {
        Task<ItemAttachmentWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<ItemAttachmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string description = null,
            string url = null,
            bool? active = null,
            Guid? itemId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<ItemAttachment>> GetListAsync(
                    string filterText = null,
                    string description = null,
                    string url = null,
                    bool? active = null,
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
            Guid? itemId = null,
            CancellationToken cancellationToken = default);
    }
}