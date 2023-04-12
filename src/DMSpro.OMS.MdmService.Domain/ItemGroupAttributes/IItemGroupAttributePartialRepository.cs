using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.ItemGroupAttributes
{
	public partial interface IItemGroupAttributeRepository : IRepository<ItemGroupAttribute, Guid>
    {
        Task<List<ItemGroupAttribute>> GetByIdAsync(List<Guid> ids);
    }
}