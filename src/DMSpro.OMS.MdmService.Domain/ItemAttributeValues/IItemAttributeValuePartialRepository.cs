using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
	public partial interface IItemAttributeValueRepository
	{
		Task<List<ItemAttributeValue>> GetByIdAsync(List<Guid> ids);
    }
}