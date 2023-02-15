using Volo.Abp.Caching;
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
		private readonly IDistributedCache<SalesOrgHeaderExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly SalesOrgHeaderManager _salesOrgHeaderManager;

		public SalesOrgHeadersAppService(ICurrentTenant currentTenant,
			ISalesOrgHeaderRepository repository,
			SalesOrgHeaderManager salesOrgHeaderManager,
			IConfiguration settingProvider,
			IDistributedCache<SalesOrgHeaderExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider)
		{
			_salesOrgHeaderRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_salesOrgHeaderManager = salesOrgHeaderManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISalesOrgHeaderRepository", _salesOrgHeaderRepository));
		}
    }
}