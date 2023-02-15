using DMSpro.OMS.MdmService.Partial;
using Volo.Abp.Caching;
using Volo.Abp.MultiTenancy;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.MCPDetails;
using DMSpro.OMS.MdmService.VisitPlans;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    [Authorize(MdmServicePermissions.MCPHeaders.Default)]
    public partial class MCPHeadersAppService : PartialAppService<MCPHeader, MCPHeaderDto, IMCPHeaderRepository>, IMCPHeadersAppService
    {
        private readonly IMCPHeaderRepository _mCPHeaderRepository;
        private readonly IDistributedCache<MCPHeaderExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly MCPHeaderManager _mCPHeaderManager;

        private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IItemGroupRepository _itemGroupRepository;
        private readonly IMCPDetailRepository _mCPDetailRepository;
        private readonly IVisitPlanRepository _visitPlanRepository;

        public MCPHeadersAppService(ICurrentTenant currentTenant,
            IMCPHeaderRepository repository,
            MCPHeaderManager companyManager,
            ISalesOrgHierarchyRepository salesOrgHierarchyRepository,
            ICompanyRepository companyRepository,
            IItemGroupRepository itemGroupRepository,
            IMCPDetailRepository mCPDetailRepository,
            IVisitPlanRepository visitPlanRepository,
        IConfiguration settingProvider,
            IDistributedCache<MCPHeaderExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _mCPHeaderRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _mCPHeaderManager = companyManager;

            _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
            _companyRepository = companyRepository;
            _itemGroupRepository = itemGroupRepository;
            _repositories.Add("ISalesOrgHierarchyRepository", _salesOrgHierarchyRepository);
            _repositories.Add("ICompanyRepository", _companyRepository);
            _repositories.Add("IItemGroupRepository", _itemGroupRepository);
            _repositories.Add("IMCPHeaderRepository", _mCPHeaderRepository);

            _mCPDetailRepository = mCPDetailRepository;
            _visitPlanRepository = visitPlanRepository;
        }
    }
}