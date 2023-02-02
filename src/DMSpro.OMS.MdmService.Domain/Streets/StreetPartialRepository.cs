using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Streets
{
	public partial interface IStreetRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}