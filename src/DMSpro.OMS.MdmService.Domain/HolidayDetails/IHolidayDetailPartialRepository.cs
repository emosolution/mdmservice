using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
	public partial interface IHolidayDetailRepository
	{
		Task<List<HolidayDetail>> GetByIdAsync(List<Guid> ids);
    }
}