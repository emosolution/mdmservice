using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Routes
{
	public partial interface IRouteRepository
	{
		Task<List<Route>> GetByIdAsync(List<Guid> ids);
    }
}