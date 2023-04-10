using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.WorkingPositions;
using DMSpro.OMS.MdmService.EmployeeAttachments;
using DMSpro.OMS.MdmService.EmployeeImages;
using DMSpro.OMS.MdmService.NumberingConfigDetails;
using DMSpro.OMS.MdmService.Companies;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    [Authorize(MdmServicePermissions.EmployeeProfiles.Default)]
    public partial class EmployeeProfilesAppService : PartialAppService<EmployeeProfile, EmployeeProfileWithDetailsDto, IEmployeeProfileRepository>,
        IEmployeeProfilesAppService
    {
        private readonly IEmployeeProfileRepository _employeeProfileRepository;
        private readonly EmployeeProfileManager _employeeProfileManager;
        private readonly IEmployeeAttachmentRepository _employeeAttachmentRepository;
        private readonly IEmployeeImageRepository _employeeImageRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly INumberingConfigDetailsInternalAppService _numberingConfigDetailsInternalAppService;


        private readonly IWorkingPositionRepository _workingPositionRepository;

        public EmployeeProfilesAppService(ICurrentTenant currentTenant,
            IEmployeeProfileRepository repository,
            EmployeeProfileManager employeeProfileManager,
            IEmployeeAttachmentRepository employeeAttachmentRepository,
            IEmployeeImageRepository employeeImageRepository,
            ICompanyRepository companyRepository,
            INumberingConfigDetailsInternalAppService numberingConfigDetailsInternalAppService,
            IConfiguration settingProvider,
            IWorkingPositionRepository workingPositionRepository)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.EmployeeProfiles.Default)
        {
            _employeeProfileRepository = repository;
            _employeeProfileManager = employeeProfileManager;
            _employeeAttachmentRepository = employeeAttachmentRepository;
            _employeeImageRepository = employeeImageRepository;
            _companyRepository = companyRepository;
            _numberingConfigDetailsInternalAppService = numberingConfigDetailsInternalAppService;

            _workingPositionRepository = workingPositionRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IEmployeeProfileRepository", _employeeProfileRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IWorkingPositionRepository", _workingPositionRepository));
        }
    }
}