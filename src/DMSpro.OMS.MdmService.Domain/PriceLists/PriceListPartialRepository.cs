using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.PriceLists
{
	public partial interface IPriceListRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}