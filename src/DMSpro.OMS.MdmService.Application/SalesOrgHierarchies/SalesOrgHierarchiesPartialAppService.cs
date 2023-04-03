using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.SalesOrgHeaders;
using DMSpro.OMS.MdmService.NumberingConfigDetails;
using DMSpro.OMS.MdmService.CompanyIdentityUserAssignments;
using DMSpro.OMS.MdmService.Companies;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
	[Authorize(MdmServicePermissions.SalesOrgHierarchies.Default)]
	public partial class SalesOrgHierarchiesAppService : PartialAppService<SalesOrgHierarchy, SalesOrgHierarchyWithDetailsDto, ISalesOrgHierarchyRepository>,
		ISalesOrgHierarchiesAppService
	{
		private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;
		private readonly INumberingConfigDetailsInternalAppService _numberingConfigDetailsInternalAppService;
        private readonly ISalesOrgHierarchiesInternalAppService _salesOrgHierarchiesInternalAppService;
		private readonly ICompanyRepository _companyRepository;

		private readonly ISalesOrgHeaderRepository _salesOrgHeaderRepository;

		public SalesOrgHierarchiesAppService(ICurrentTenant currentTenant,
			ISalesOrgHierarchyRepository repository,
			INumberingConfigDetailsInternalAppService numberingConfigDetailsInternalAppService,
			ISalesOrgHierarchiesInternalAppService salesOrgHierarchiesInternalAppService,
            ICompanyRepository companyRepository,
            IConfiguration settingProvider,
			ISalesOrgHeaderRepository salesOrgHeaderRepository)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.SalesOrgHierarchies.Default)
		{
			_salesOrgHierarchyRepository = repository;
            _numberingConfigDetailsInternalAppService = numberingConfigDetailsInternalAppService;
            _salesOrgHierarchiesInternalAppService = salesOrgHierarchiesInternalAppService;
            _salesOrgHeaderRepository = salesOrgHeaderRepository;
			_companyRepository = companyRepository;

			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISalesOrgHierarchyRepository", _salesOrgHierarchyRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISalesOrgHeaderRepository", _salesOrgHeaderRepository));
        }
    }
}