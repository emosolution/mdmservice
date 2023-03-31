using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.MCPDetails;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;

namespace DMSpro.OMS.MdmService.VisitPlans
{
    [Authorize(MdmServicePermissions.VisitPlans.Default)]
    public partial class VisitPlansAppService : PartialAppService<VisitPlan, VisitPlanWithDetailsDto, IVisitPlanRepository>,
        IVisitPlansAppService
    {
        private readonly IVisitPlanRepository _visitPlanRepository;
        private readonly VisitPlanManager _visitPlanManager;

        private readonly IVisitPlansScheduledAppService _visitPlansScheduledAppService;
        private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;
        private readonly IMCPDetailRepository _mCPDetailRepository;
        private readonly IItemGroupRepository _itemGroupRepository;
        private readonly ICustomerRepository _customerRepository;

        public VisitPlansAppService(ICurrentTenant currentTenant,
            IVisitPlanRepository repository,
            VisitPlanManager visitPlanManager,
            IConfiguration settingProvider,
            IVisitPlansScheduledAppService visitPlansScheduledAppService,
            ISalesOrgHierarchyRepository salesOrgHierarchyRepository,
            IMCPDetailRepository mCPDetailRepository,
            IItemGroupRepository itemGroupRepository,
            ICustomerRepository customerRepository,
            ICompanyRepository companyRepository)
            : base(currentTenant, repository, settingProvider, MdmServicePermissions.VisitPlans.Default)
        {
            _visitPlanRepository = repository;
            _visitPlanManager = visitPlanManager;

            _visitPlansScheduledAppService = visitPlansScheduledAppService;
            _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
            _mCPDetailRepository = mCPDetailRepository;
            _itemGroupRepository = itemGroupRepository;
            _customerRepository = customerRepository;

            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IVisitPlanRepository", _visitPlanRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IVisitPlansScheduledAppService", _visitPlansScheduledAppService));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISalesOrgHierarchyRepository", _salesOrgHierarchyRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IMCPDetailRepository", _mCPDetailRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IItemGroupRepository", _itemGroupRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ICustomerRepository", _customerRepository));
        }
    }
}