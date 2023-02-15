using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.SalesOrgHeaders;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
	[Authorize(MdmServicePermissions.SalesOrgHierarchies.Default)]
	public partial class SalesOrgHierarchiesAppService : PartialAppService<SalesOrgHierarchy, SalesOrgHierarchyDto, ISalesOrgHierarchyRepository>,
		ISalesOrgHierarchiesAppService
	{
		private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;
		private readonly IDistributedCache<SalesOrgHierarchyExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly SalesOrgHierarchyManager _salesOrgHierarchyManager;

		private readonly ISalesOrgHeaderRepository _salesOrgHeaderRepository;

		public SalesOrgHierarchiesAppService(ICurrentTenant currentTenant,
			ISalesOrgHierarchyRepository repository,
			SalesOrgHierarchyManager salesOrgHierarchyManager,
			IConfiguration settingProvider,
			ISalesOrgHeaderRepository salesOrgHeaderRepository,
			IDistributedCache<SalesOrgHierarchyExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider)
		{
			_salesOrgHierarchyRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_salesOrgHierarchyManager = salesOrgHierarchyManager;
			
			_salesOrgHeaderRepository = salesOrgHeaderRepository;

			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISalesOrgHierarchyRepository", _salesOrgHierarchyRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISalesOrgHeaderRepository", _salesOrgHeaderRepository));
        }
    }
}