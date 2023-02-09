using System.Collections.Generic;
using DMSpro.OMS.MdmService.Companies;
using Volo.Abp.Caching;
using Volo.Abp.MultiTenancy;
using Microsoft.Extensions.Configuration;

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

        public CompanyIdentityUserAssignmentsAppService(ICurrentTenant currentTenant,
            ICompanyIdentityUserAssignmentRepository repository,
            CompanyIdentityUserAssignmentManager companyIdentityUserAssignmentManager,
            IConfiguration settingProvider,
            ICompanyRepository companyRepository,
            IDistributedCache<CompanyIdentityUserAssignmentExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _companyIdentityUserAssignmentRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _companyIdentityUserAssignmentManager = companyIdentityUserAssignmentManager;
            _companyRepository = companyRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICompanyRepository", _companyRepository));
        }
	}
}