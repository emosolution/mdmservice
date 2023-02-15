using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
	public partial interface IItemAttributeRepository
	{
		Task<List<ItemAttribute>> GetByIdAsync(List<Guid> ids);
    }
}