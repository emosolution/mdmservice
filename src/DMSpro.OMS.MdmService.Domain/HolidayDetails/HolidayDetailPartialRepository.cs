using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
	public partial interface IHolidayDetailRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}