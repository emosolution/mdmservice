using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    public interface ICustomerAttachmentRepository : IRepository<CustomerAttachment, Guid>
    {
        Task<CustomerAttachmentWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<CustomerAttachmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string url = null,
            string description = null,
            bool? active = null,
            Guid? customerId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<CustomerAttachment>> GetListAsync(
                    string filterText = null,
                    string url = null,
                    string description = null,
                    bool? active = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string url = null,
            string description = null,
            bool? active = null,
            Guid? customerId = null,
            CancellationToken cancellationToken = default);
    }
}