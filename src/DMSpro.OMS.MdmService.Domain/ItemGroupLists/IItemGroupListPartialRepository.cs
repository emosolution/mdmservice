using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
	public partial interface IItemGroupListRepository
	{
		Task<List<ItemGroupList>> GetByIdAsync(List<Guid> ids);
    }
}