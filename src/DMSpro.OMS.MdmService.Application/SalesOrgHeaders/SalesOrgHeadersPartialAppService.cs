using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
	[Authorize(MdmServicePermissions.SalesOrgHeaders.Default)]
	public partial class SalesOrgHeadersAppService : PartialAppService<SalesOrgHeader, SalesOrgHeaderDto, ISalesOrgHeaderRepository>,
		ISalesOrgHeadersAppService
	{
		private readonly ISalesOrgHeaderRepository _salesOrgHeaderRepository;
		private readonly SalesOrgHeaderManager _salesOrgHeaderManager;

		public SalesOrgHeadersAppService(ICurrentTenant currentTenant,
			ISalesOrgHeaderRepository repository,
			SalesOrgHeaderManager salesOrgHeaderManager,
			IConfiguration settingProvider)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.SalesOrgHeaders.Default)
		{
			_salesOrgHeaderRepository = repository;
			_salesOrgHeaderManager = salesOrgHeaderManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISalesOrgHeaderRepository", _salesOrgHeaderRepository));
		}
    }
}