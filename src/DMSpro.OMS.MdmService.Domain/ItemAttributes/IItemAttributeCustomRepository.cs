using DMSpro.OMS.MdmService.CustomerAttributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public interface IItemAttributeCustomRepository : IRepository<ItemAttribute, Guid>
    {
        Task<bool> CreateWithExcepAsync(List<ItemAttribute> seedData);
    }
}
