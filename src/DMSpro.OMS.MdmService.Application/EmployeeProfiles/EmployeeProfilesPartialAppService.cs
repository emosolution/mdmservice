using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.WorkingPositions;
using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.EmployeeAttachments;
using DMSpro.OMS.MdmService.EmployeeImages;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    [Authorize(MdmServicePermissions.EmployeeProfiles.Default)]
    public partial class EmployeeProfilesAppService : PartialAppService<EmployeeProfile, EmployeeProfileWithDetailsDto, IEmployeeProfileRepository>,
        IEmployeeProfilesAppService
    {
        private readonly IEmployeeProfileRepository _employeeProfileRepository;
        private readonly IDistributedCache<EmployeeProfileExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly EmployeeProfileManager _employeeProfileManager;
        private readonly IEmployeeAttachmentRepository _employeeAttachmentRepository;
        private readonly IEmployeeImageRepository _employeeImageRepository;


        private readonly IWorkingPositionRepository _workingPositionRepository;
        private readonly ISystemDataRepository _systemDataRepository;

        public EmployeeProfilesAppService(ICurrentTenant currentTenant,
            IEmployeeProfileRepository repository,
            EmployeeProfileManager employeeProfileManager,
            IEmployeeAttachmentRepository employeeAttachmentRepository,
            IEmployeeImageRepository employeeImageRepository,
            IConfiguration settingProvider,
            IWorkingPositionRepository workingPositionRepository,
            ISystemDataRepository systemDataRepository,
            IDistributedCache<EmployeeProfileExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.EmployeeProfiles.Default)
        {
            _employeeProfileRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _employeeProfileManager = employeeProfileManager;
            _employeeAttachmentRepository = employeeAttachmentRepository;
            _employeeImageRepository = employeeImageRepository;

            _workingPositionRepository = workingPositionRepository;
            _systemDataRepository = systemDataRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IEmployeeProfileRepository", _employeeProfileRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IWorkingPositionRepository", _workingPositionRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISystemDataRepository", _systemDataRepository));
        }
    }
}