using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
    public partial interface IEmployeeImageRepository : IRepository<EmployeeImage, Guid>
    {
        Task<EmployeeImageWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<EmployeeImageWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string description = null,
            string url = null,
            bool? active = null,
            bool? isAvatar = null,
            Guid? employeeProfileId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<EmployeeImage>> GetListAsync(
                    string filterText = null,
                    string description = null,
                    string url = null,
                    bool? active = null,
                    bool? isAvatar = null,
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
            bool? isAvatar = null,
            Guid? employeeProfileId = null,
            CancellationToken cancellationToken = default);
    }
}