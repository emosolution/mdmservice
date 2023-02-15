using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
	public partial interface IUOMGroupDetailRepository
	{
		Task<List<UOMGroupDetail>> GetByIdAsync(List<Guid> ids);
    }
}