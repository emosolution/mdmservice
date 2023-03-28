using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.SalesChannels
{
	[Authorize(MdmServicePermissions.SalesChannels.Default)]
	public partial class SalesChannelsAppService : PartialAppService<SalesChannel, SalesChannelDto, ISalesChannelRepository>,
		ISalesChannelsAppService
	{
		private readonly ISalesChannelRepository _salesChannelRepository;
		private readonly IDistributedCache<SalesChannelExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly SalesChannelManager _salesChannelManager;

		public SalesChannelsAppService(ICurrentTenant currentTenant,
			ISalesChannelRepository repository,
			SalesChannelManager salesChannelManager,
			IConfiguration settingProvider,
			IDistributedCache<SalesChannelExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.SalesChannels.Default)
		{
			_salesChannelRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_salesChannelManager = salesChannelManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("ISalesChannelRepository", _salesChannelRepository));
		}
    }
}