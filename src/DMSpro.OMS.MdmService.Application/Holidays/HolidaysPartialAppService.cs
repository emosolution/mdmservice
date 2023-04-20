using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.MultiTenancy;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DMSpro.OMS.MdmService.Partial;
using DMSpro.OMS.MdmService.HolidayDetails;

namespace DMSpro.OMS.MdmService.Holidays
{
	[Authorize(MdmServicePermissions.Holidays.Default)]
	public partial class HolidaysAppService : PartialAppService<Holiday, HolidayDto, IHolidayRepository>,
		IHolidaysAppService
	{
		private readonly IHolidayRepository _holidayRepository;
        private readonly IHolidayDetailRepository _holidayDetailRepository;

        public HolidaysAppService(ICurrentTenant currentTenant,
			IHolidayRepository repository,
            IHolidayDetailRepository holidayDetailRepository,
			IConfiguration settingProvider)
			: base(currentTenant, repository, settingProvider, MdmServicePermissions.Holidays.Default)
		{
			_holidayRepository = repository;
            _holidayDetailRepository = holidayDetailRepository;
            _repositories.AddIfNotContains(
                new KeyValuePair<string, object>("IHolidayRepository", _holidayRepository));
		}
    }
}