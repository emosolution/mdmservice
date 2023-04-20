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

		private readonly IHolidayRepository _holidayRepository;

		public HolidayDetailsAppService(ICurrentTenant currentTenant,
			IHolidayDetailRepository repository,
			IConfiguration settingProvider,
			IHolidayRepository holidayRepository)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.HolidayDetails.Default)
		{
			_holidayDetailRepository = repository;
			
			_holidayRepository = holidayRepository;

			_repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IHolidayDetailRepository", _holidayDetailRepository));
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IHolidayRepository", _holidayRepository));
        }
    }
}