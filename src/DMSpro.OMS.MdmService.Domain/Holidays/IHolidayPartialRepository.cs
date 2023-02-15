using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Holidays
{
	public partial interface IHolidayRepository
	{
		Task<List<Holiday>> GetByIdAsync(List<Guid> ids);
    }
}