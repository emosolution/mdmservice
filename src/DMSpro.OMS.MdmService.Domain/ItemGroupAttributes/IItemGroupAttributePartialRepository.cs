using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemGroupAttributes
{
	public partial interface IItemGroupAttributeRepository
	{
		Task<List<ItemGroupAttribute>> GetByIdAsync(List<Guid> ids);
    }
}