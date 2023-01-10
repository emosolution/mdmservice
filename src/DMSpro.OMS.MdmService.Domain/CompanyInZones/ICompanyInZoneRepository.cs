using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CompanyInZones
{
    public interface ICompanyInZoneRepository : IRepository<CompanyInZone, Guid>
    {
        Task<CompanyInZoneWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<CompanyInZoneWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            bool? isBase = null,
            Guid? salesOrgHierarchyId = null,
            Guid? companyId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<CompanyInZone>> GetListAsync(
                    string filterText = null,
                    DateTime? effectiveDateMin = null,
                    DateTime? effectiveDateMax = null,
                    DateTime? endDateMin = null,
                    DateTime? endDateMax = null,
                    bool? isBase = null,
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
            bool? isBase = null,
            Guid? salesOrgHierarchyId = null,
            Guid? companyId = null,
            CancellationToken cancellationToken = default);
    }
}