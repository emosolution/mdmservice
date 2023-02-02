using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
	public partial interface IPriceUpdateDetailRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}