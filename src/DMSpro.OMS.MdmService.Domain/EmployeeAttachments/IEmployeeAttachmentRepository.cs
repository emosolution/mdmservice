using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.EmployeeAttachments
{
    public partial interface IEmployeeAttachmentRepository : IRepository<EmployeeAttachment, Guid>
    {
        Task<EmployeeAttachmentWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<EmployeeAttachmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string description = null,
            bool? active = null,
            Guid? fileId = null,
            Guid? employeeProfileId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<EmployeeAttachment>> GetListAsync(
                    string filterText = null,
                    string description = null,
                    bool? active = null,
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
            Guid? fileId = null,
            Guid? employeeProfileId = null,
            CancellationToken cancellationToken = default);
    }
}