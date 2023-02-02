using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
	public partial interface IPriceUpdateRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}