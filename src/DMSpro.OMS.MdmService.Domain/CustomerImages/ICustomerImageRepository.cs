using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CustomerImages
{
    public partial interface ICustomerImageRepository : IRepository<CustomerImage, Guid>
    {
        Task<CustomerImageWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<CustomerImageWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string description = null,
            bool? active = null,
            bool? isAvatar = null,
            bool? isPOSM = null,
            Guid? fileId = null,
            Guid? customerId = null,
            Guid? pOSMItemId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<CustomerImage>> GetListAsync(
                    string filterText = null,
                    string description = null,
                    bool? active = null,
                    bool? isAvatar = null,
                    bool? isPOSM = null,
                    Guid? fileId = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string description = null,
            bool? active = null,
            bool? isAvatar = null,
            bool? isPOSM = null,
            Guid? fileId = null,
            Guid? customerId = null,
            Guid? pOSMItemId = null,
            CancellationToken cancellationToken = default);
    }
}