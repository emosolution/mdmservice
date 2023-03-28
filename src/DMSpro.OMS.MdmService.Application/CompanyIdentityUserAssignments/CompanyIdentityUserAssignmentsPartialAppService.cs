using System.Collections.Generic;
using DMSpro.OMS.MdmService.Companies;
using Volo.Abp.Caching;
using Volo.Abp.MultiTenancy;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using Volo.Abp.Users;
using DMSpro.OMS.MdmService.Permissions;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public partial class CompanyIdentityUserAssignmentsAppService :
        PartialAppService<CompanyIdentityUserAssignment, CompanyIdentityUserAssignmentDto, ICompanyIdentityUserAssignmentRepository>,
        ICompanyIdentityUserAssignmentsAppService
    {
        private readonly ICompanyIdentityUserAssignmentRepository _companyIdentityUserAssignmentRepository;
        private readonly IDistributedCache<CompanyIdentityUserAssignmentExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly CompanyIdentityUserAssignmentManager _companyIdentityUserAssignmentManager;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompaniesInternalAppService _companiesInternalAppService;
        private readonly ICurrentUser _currentUser;
        private readonly ICompanyIdentityUserAssignmentsInternalAppService _companyIdentityUserAssignmentsInternalAppService;

        public CompanyIdentityUserAssignmentsAppService(ICurrentTenant currentTenant,
            ICompanyIdentityUserAssignmentRepository repository,
            CompanyIdentityUserAssignmentManager companyIdentityUserAssignmentManager,
            IConfiguration settingProvider,
            ICompanyRepository companyRepository,
            ICompaniesInternalAppService companiesInternalAppService,
            ICurrentUser currentUser,
            IDistributedCache<CompanyIdentityUserAssignmentExcelDownloadTokenCacheItem, string> excelDownloadTokenCache,
            ICompanyIdentityUserAssignmentsInternalAppService companyIdentityUserAssignmentsInternalAppService)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.CompanyIdentityUserAssignments.Default)
        {
            _companyIdentityUserAssignmentRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _companyIdentityUserAssignmentManager = companyIdentityUserAssignmentManager;
            _companyIdentityUserAssignmentsInternalAppService = companyIdentityUserAssignmentsInternalAppService;
            _currentUser = currentUser;

            _companyRepository = companyRepository;
            _companiesInternalAppService = companiesInternalAppService;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICompanyIdentityUserAssignmentRepository", _companyIdentityUserAssignmentRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICompanyRepository", _companyRepository));
        }
    }
}