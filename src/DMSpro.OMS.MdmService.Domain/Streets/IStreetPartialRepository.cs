using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Streets
{
	public partial interface IStreetRepository
	{
		Task<List<Street>> GetByIdAsync(List<Guid> ids);
    }
}