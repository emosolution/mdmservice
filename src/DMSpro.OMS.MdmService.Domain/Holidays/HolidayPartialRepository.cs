using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Holidays
{
	public partial interface IHolidayRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}