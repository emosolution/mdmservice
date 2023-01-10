using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.MCPDetails;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using System;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    public partial class VisitPlansAppService
    {
        private readonly IVisitPlansScheduledAppService _visitPlansScheduledAppService;
        private readonly IDistributedCache<VisitPlanExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IVisitPlanRepository _visitPlanRepository;
        private readonly VisitPlanManager _visitPlanManager;
        private readonly IRepository<MCPDetail, Guid> _mCPDetailRepository;
        private readonly IRepository<Customer, Guid> _customerRepository;
        private readonly IRepository<SalesOrgHierarchy, Guid> _salesOrgHierarchyRepository;
        private readonly IRepository<Company, Guid> _companyRepository;
        private readonly IRepository<ItemGroup, Guid> _itemGroupRepository;

        public VisitPlansAppService(
            IVisitPlansScheduledAppService visitPlansScheduledAppService,
            IVisitPlanRepository visitPlanRepository,
            VisitPlanManager visitPlanManager, IDistributedCache<VisitPlanExcelDownloadTokenCacheItem, string> excelDownloadTokenCache,
            IRepository<MCPDetail, Guid> mCPDetailRepository, IRepository<Customer, Guid> customerRepository,
            IRepository<SalesOrgHierarchy, Guid> salesOrgHierarchyRepository, IRepository<Company, Guid> companyRepository,
            IRepository<ItemGroup, Guid> itemGroupRepository)
        {
            _visitPlansScheduledAppService = visitPlansScheduledAppService;
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _visitPlanRepository = visitPlanRepository;
            _visitPlanManager = visitPlanManager; _mCPDetailRepository = mCPDetailRepository;
            _customerRepository = customerRepository;
            _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
            _companyRepository = companyRepository;
            _itemGroupRepository = itemGroupRepository;
        }

        [Authorize(MdmServicePermissions.VisitPlans.GenerateVisitPlanFromMCP)]
        public async Task<List<VisitPlanDto>> GenerateWithPermissionAsync(VisitPlanGenerationInputDto input)
        {
            return await _visitPlansScheduledAppService.GenerateAsync(input);
        }
    }
}