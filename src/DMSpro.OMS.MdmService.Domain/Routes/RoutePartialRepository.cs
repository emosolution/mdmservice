using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Routes
{
	public partial interface IRouteRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}