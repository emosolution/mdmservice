using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
	public partial interface IHolidayDetailRepository : IRepository<HolidayDetail, Guid>
    {
		Task<List<HolidayDetail>> GetByIdAsync(List<Guid> ids);
    }
}