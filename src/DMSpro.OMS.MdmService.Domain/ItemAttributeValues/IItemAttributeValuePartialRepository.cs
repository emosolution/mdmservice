using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public partial interface IItemAttributeValueRepository : IRepository<ItemAttributeValue, Guid>
    {
        Task<List<ItemAttributeValue>> GetByIdAsync(List<Guid> ids);
    }
}