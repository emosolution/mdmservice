using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
	[Authorize(MdmServicePermissions.SalesOrgHeaders.Default)]
	public partial class SalesOrgHeadersAppService : PartialAppService<SalesOrgHeader, SalesOrgHeaderDto, ISalesOrgHeaderRepository>,
		ISalesOrgHeadersAppService
	{
		private readonly ISalesOrgHeaderRepository _salesOrgHeaderRepository;
        private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;

        public SalesOrgHeadersAppService(ICurrentTenant currentTenant,
			ISalesOrgHeaderRepository repository,
			ISalesOrgHierarchyRepository salesOrgHierarchyRepository,
			IConfiguration settingProvider)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.SalesOrgHeaders.Default)
		{
			_salesOrgHeaderRepository = repository;
            _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISalesOrgHeaderRepository", _salesOrgHeaderRepository));
		}
    }
}