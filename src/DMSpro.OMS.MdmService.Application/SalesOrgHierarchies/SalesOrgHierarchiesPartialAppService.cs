using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.SalesOrgHeaders;
using DMSpro.OMS.MdmService.NumberingConfigDetails;
using DMSpro.OMS.MdmService.CompanyInZones;
using DMSpro.OMS.MdmService.CompanyIdentityUserAssignments;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
	[Authorize(MdmServicePermissions.SalesOrgHierarchies.Default)]
	public partial class SalesOrgHierarchiesAppService : PartialAppService<SalesOrgHierarchy, SalesOrgHierarchyWithDetailsDto, ISalesOrgHierarchyRepository>,
		ISalesOrgHierarchiesAppService
	{
		private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;
		private readonly SalesOrgHierarchyManager _salesOrgHierarchyManager;
		private readonly INumberingConfigDetailsInternalAppService _numberingConfigDetailsInternalAppService;
		private readonly ICompanyIdentityUserAssignmentsInternalAppService _companyIdentityUserAssignmentsInternalAppService;

		private readonly ISalesOrgHeaderRepository _salesOrgHeaderRepository;

		public SalesOrgHierarchiesAppService(ICurrentTenant currentTenant,
			ISalesOrgHierarchyRepository repository,
			SalesOrgHierarchyManager salesOrgHierarchyManager,
			INumberingConfigDetailsInternalAppService numberingConfigDetailsInternalAppService,
            ICompanyIdentityUserAssignmentsInternalAppService companyIdentityUserAssignmentsInternalAppService,
            IConfiguration settingProvider,
			ISalesOrgHeaderRepository salesOrgHeaderRepository)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.SalesOrgHierarchies.Default)
		{
			_salesOrgHierarchyRepository = repository;
			_salesOrgHierarchyManager = salesOrgHierarchyManager;
            _numberingConfigDetailsInternalAppService = numberingConfigDetailsInternalAppService;
            _companyIdentityUserAssignmentsInternalAppService = companyIdentityUserAssignmentsInternalAppService;

            _salesOrgHeaderRepository = salesOrgHeaderRepository;

			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISalesOrgHierarchyRepository", _salesOrgHierarchyRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISalesOrgHeaderRepository", _salesOrgHeaderRepository));
        }
    }
}