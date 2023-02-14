using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Caching;
using Volo.Abp.MultiTenancy;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
	[Authorize(MdmServicePermissions.SalesOrgHeaders.Default)]
	public partial class SalesOrgHeadersAppService : PartialAppService<SalesOrgHeader, SalesOrgHeaderDto, ISalesOrgHeaderRepository>
		, ISalesOrgHeadersAppService
		{
		private readonly ISalesOrgHeaderRepository _salesOrgHeaderRepository;
		private readonly SalesOrgHeaderManager _salesOrgHeaderManager;
		private readonly IDistributedCache<SalesOrgHeaderExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;

		public SalesOrgHeadersAppService(ICurrentTenant currentTenant,
			ISalesOrgHeaderRepository repository,
			SalesOrgHeaderManager manager,
			IConfiguration settingProvider,
			IDistributedCache<SalesOrgHeaderExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(currentTenant, repository, settingProvider)
        {
            _salesOrgHeaderRepository = repository;
            _salesOrgHeaderManager = manager;
            _excelDownloadTokenCache = excelDownloadTokenCache;

			_repositories.Add("ISalesOrgHeaderRepository", _salesOrgHeaderRepository);
        }
    }
}