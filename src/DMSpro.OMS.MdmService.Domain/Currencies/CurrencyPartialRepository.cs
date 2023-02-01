using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Currencies
{
	public partial interface ICurrencyRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}