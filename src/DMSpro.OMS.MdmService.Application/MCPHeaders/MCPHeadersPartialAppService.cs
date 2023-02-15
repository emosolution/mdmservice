using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.VisitPlans;
using DMSpro.OMS.MdmService.MCPDetails;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    [Authorize(MdmServicePermissions.MCPs.Default)]
    public partial class MCPHeadersAppService : PartialAppService<MCPHeader, MCPHeaderDto, IMCPHeaderRepository>,
        IMCPHeadersAppService
    {
        private readonly IMCPHeaderRepository _mCPHeaderRepository;
        private readonly IDistributedCache<MCPHeaderExcelDownloadTokenCacheItem, string>
            _excelDownloadTokenCache;
        private readonly MCPHeaderManager _mCPHeaderManager;

        private readonly IVisitPlanRepository _visitPlanRepository;
        private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;
        private readonly IMCPDetailRepository _mCPDetailRepository;
        private readonly IItemGroupRepository _itemGroupRepository;
        private readonly ICompanyRepository _companyRepository;

        public MCPHeadersAppService(ICurrentTenant currentTenant,
            IMCPHeaderRepository repository,
            MCPHeaderManager mCPHeaderManager,
            IConfiguration settingProvider,
            IVisitPlanRepository visitPlanRepository,
            ISalesOrgHierarchyRepository salesOrgHierarchyRepository,
            IMCPDetailRepository mCPDetailRepository,
            IItemGroupRepository itemGroupRepository,
            ICompanyRepository companyRepository,
            IDistributedCache<MCPHeaderExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _mCPHeaderRepository = repository;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _mCPHeaderManager = mCPHeaderManager;

            _visitPlanRepository = visitPlanRepository;
            _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
            _mCPDetailRepository = mCPDetailRepository;
            _itemGroupRepository = itemGroupRepository;
            _companyRepository = companyRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IMCPHeaderRepository", _mCPHeaderRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IVisitPlanRepository", _visitPlanRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISalesOrgHierarchyRepository", _salesOrgHierarchyRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IMCPDetailRepository", _mCPDetailRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemGroupRepository", _itemGroupRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICompanyRepository", _companyRepository));
        } 
    }
}