using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemImages
{
	public partial interface IItemImageRepository
	{
		Task<List<ItemImage>> GetByIdAsync(List<Guid> ids);
    }
}