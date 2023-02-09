using DMSpro.OMS.MdmService.GeoMasters;
using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Uow;

namespace DMSpro.OMS.MdmService.Companies
{
    [Authorize(MdmServicePermissions.CompanyMasters.Default)]
    public partial class CompaniesAppService : PartialAppService<Company, CompanyDto, ICompanyRepository>, ICompaniesAppService
    {
        private readonly IGeoMasterRepository _geoMasterRepository;

        private readonly ICompanyRepository _companyRepository;
        private readonly IDistributedCache<CompanyExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly CompanyManager _companyManager;

        public CompaniesAppService(ICurrentTenant currentTenant,
            IGeoMasterRepository geoMasterRepository,
            ICompanyRepository repository,
            CompanyManager companyManager,
            IConfiguration settingProvider,
            IDistributedCache<CompanyExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _companyRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _companyManager = companyManager;
            _geoMasterRepository = geoMasterRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IGeoMasterRepository", _geoMasterRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICompanyRepository", _companyRepository));
        }
    }
}
