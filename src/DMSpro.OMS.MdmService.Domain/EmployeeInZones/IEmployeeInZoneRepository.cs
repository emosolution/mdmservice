using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.EmployeeInZones
{
    public partial interface IEmployeeInZoneRepository : IRepository<EmployeeInZone, Guid>
    {
        Task<EmployeeInZoneWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<EmployeeInZoneWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            Guid? salesOrgHierarchyId = null,
            Guid? employeeId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<EmployeeInZone>> GetListAsync(
                    string filterText = null,
                    DateTime? effectiveDateMin = null,
                    DateTime? effectiveDateMax = null,
                    DateTime? endDateMin = null,
                    DateTime? endDateMax = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            Guid? salesOrgHierarchyId = null,
            Guid? employeeId = null,
            CancellationToken cancellationToken = default);
    }
}