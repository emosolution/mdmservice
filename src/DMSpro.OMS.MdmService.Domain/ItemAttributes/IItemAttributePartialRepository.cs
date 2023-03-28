using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
	public partial interface IItemAttributeRepository : IRepository<ItemAttribute, Guid>
    {
		Task<List<ItemAttribute>> GetByIdAsync(List<Guid> ids);
    }
}