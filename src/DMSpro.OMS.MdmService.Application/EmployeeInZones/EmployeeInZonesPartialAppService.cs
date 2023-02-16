using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.EmployeeProfiles;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;

namespace DMSpro.OMS.MdmService.EmployeeInZones
{
    [Authorize(MdmServicePermissions.EmployeeInZones.Default)]
    public partial class EmployeeInZonesAppService : PartialAppService<EmployeeInZone, EmployeeInZoneDto, IEmployeeInZoneRepository>,
        IEmployeeInZonesAppService
    {
        private readonly IEmployeeInZoneRepository _employeeInZoneRepository;
        private readonly IDistributedCache<EmployeeInZoneExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly EmployeeInZoneManager _employeeInZoneManager;

        private readonly IEmployeeProfileRepository _employeeProfileRepository;
        private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;

        public EmployeeInZonesAppService(ICurrentTenant currentTenant,
            IEmployeeInZoneRepository repository,
            EmployeeInZoneManager employeeInZoneManager,
            IConfiguration settingProvider,
            IEmployeeProfileRepository employeeProfileRepository,
            ISalesOrgHierarchyRepository salesOrgHierarchyRepository,
            IDistributedCache<EmployeeInZoneExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _employeeInZoneRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _employeeInZoneManager = employeeInZoneManager;

            _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
            _employeeProfileRepository = employeeProfileRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IEmployeeInZoneRepository", _employeeInZoneRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IEmployeeProfileRepository", _employeeProfileRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISalesOrgHierarchyRepository", _salesOrgHierarchyRepository));
        }
    }
}