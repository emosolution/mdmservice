using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.Holidays
{
	[Authorize(MdmServicePermissions.Holidays.Default)]
	public partial class HolidaysAppService : PartialAppService<Holiday, HolidayDto, IHolidayRepository>,
		IHolidaysAppService
	{
		private readonly IHolidayRepository _holidayRepository;
		private readonly IDistributedCache<HolidayExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly HolidayManager _holidayManager;

		public HolidaysAppService(ICurrentTenant currentTenant,
			IHolidayRepository repository,
			HolidayManager holidayManager,
			IConfiguration settingProvider,
			IDistributedCache<HolidayExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider)
		{
			_holidayRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_holidayManager = holidayManager;
			
			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IHolidayRepository", _holidayRepository));
		}
    }
}