using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
	public partial interface IItemGroupListRepository : IRepository<ItemGroupList, Guid>
    {
		Task<List<ItemGroupList>> GetByIdAsync(List<Guid> ids);
    }
}