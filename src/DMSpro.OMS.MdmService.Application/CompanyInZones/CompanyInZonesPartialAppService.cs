using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;

namespace DMSpro.OMS.MdmService.CompanyInZones
{
	[Authorize(MdmServicePermissions.CompanyInZones.Default)]
    public partial class CompanyInZonesAppService : PartialAppService<CompanyInZone, CompanyInZoneDto, ICompanyInZoneRepository>, 
        ICompanyInZonesAppService
    {
        private readonly ICompanyInZoneRepository _companyInZoneRepository;
        private readonly CompanyInZoneManager _companyInZoneManager;
        private readonly IDistributedCache<CompanyInZoneExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;

        private readonly ICompanyRepository _companyRepository;
        private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;

        public CompanyInZonesAppService(ICurrentTenant currentTenant,
            ICompanyInZoneRepository repository,
            CompanyInZoneManager manager,
            IConfiguration settingProvider,
            ICompanyRepository companyRepository,
            ISalesOrgHierarchyRepository salesOrgHierarchyRepository,
            IDistributedCache<CompanyInZoneExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _companyInZoneRepository = repository;
            _companyInZoneManager = manager;
            _excelDownloadTokenCache = excelDownloadTokenCache;

            _companyRepository = companyRepository;
            _salesOrgHierarchyRepository = salesOrgHierarchyRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICompanyInZoneRepository", _companyInZoneRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISalesOrgHierarchyRepository", _salesOrgHierarchyRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICompanyRepository", _companyRepository));
        }

		
    }
}