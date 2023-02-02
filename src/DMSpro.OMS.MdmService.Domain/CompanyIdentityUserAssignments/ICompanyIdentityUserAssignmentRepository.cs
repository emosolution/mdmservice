using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public partial interface ICompanyIdentityUserAssignmentRepository : IRepository<CompanyIdentityUserAssignment, Guid>
    {
        Task<CompanyIdentityUserAssignmentWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<CompanyIdentityUserAssignmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            Guid? identityUserId = null,
            Guid? companyId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<CompanyIdentityUserAssignment>> GetListAsync(
                    string filterText = null,
                    Guid? identityUserId = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            Guid? identityUserId = null,
            Guid? companyId = null,
            CancellationToken cancellationToken = default);
    }
}