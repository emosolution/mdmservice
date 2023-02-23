using Volo.Abp.Caching;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.Holidays;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
	[Authorize(MdmServicePermissions.HolidayDetails.Default)]
	public partial class HolidayDetailsAppService : PartialAppService<HolidayDetail, HolidayDetailWithDetailsDto, IHolidayDetailRepository>,
		IHolidayDetailsAppService
	{
		private readonly IHolidayDetailRepository _holidayDetailRepository;
		private readonly IDistributedCache<HolidayDetailExcelDownloadTokenCacheItem, string>
			_excelDownloadTokenCache;
		private readonly HolidayDetailManager _holidayDetailManager;

		private readonly IHolidayRepository _holidayRepository;

		public HolidayDetailsAppService(ICurrentTenant currentTenant,
			IHolidayDetailRepository repository,
			HolidayDetailManager holidayDetailManager,
			IConfiguration settingProvider,
			IHolidayRepository holidayRepository,
			IDistributedCache<HolidayDetailExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
			: base(currentTenant, repository, settingProvider)
		{
			_holidayDetailRepository = repository;
			_excelDownloadTokenCache = excelDownloadTokenCache;
			_holidayDetailManager = holidayDetailManager;
			
			_holidayRepository = holidayRepository;

			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IHolidayDetailRepository", _holidayDetailRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IHolidayRepository", _holidayRepository));
        }
    }
}