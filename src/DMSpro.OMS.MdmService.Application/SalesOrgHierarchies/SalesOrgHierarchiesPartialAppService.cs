using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.SalesOrgHeaders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Caching;
using Volo.Abp.MultiTenancy;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    [Authorize(MdmServicePermissions.SalesOrgHierarchies.Default)]
    public partial class SalesOrgHierarchiesAppService : PartialAppService<SalesOrgHierarchy, SalesOrgHierarchyDto, ISalesOrgHierarchyRepository>,
		ISalesOrgHierarchiesAppService
    {
        private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;
        private readonly SalesOrgHierarchyManager _salesOrgHierarchyManager;
        private readonly IDistributedCache<SalesOrgHierarchyExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;

        private readonly ISalesOrgHeaderRepository _salesOrgHeaderRepository;

        public SalesOrgHierarchiesAppService(ICurrentTenant currentTenant,
            ISalesOrgHierarchyRepository repository,
            SalesOrgHierarchyManager manager,
            IConfiguration settingProvider,
            ISalesOrgHeaderRepository salesOrgHeaderRepository,
            IDistributedCache<SalesOrgHierarchyExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _salesOrgHierarchyRepository = repository;
            _salesOrgHierarchyManager = manager;
            _excelDownloadTokenCache = excelDownloadTokenCache;

            _salesOrgHeaderRepository = salesOrgHeaderRepository;
            _repositories.Add("ISalesOrgHeaderRepository", _salesOrgHeaderRepository);
            _repositories.Add("ISalesOrgHierarchyRepository", _salesOrgHierarchyRepository);
        }
    }
}