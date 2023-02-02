using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
	public partial interface IPriceListDetailRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}