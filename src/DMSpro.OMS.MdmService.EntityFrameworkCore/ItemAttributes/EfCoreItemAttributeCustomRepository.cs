using DMSpro.OMS.MdmService.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public class EfCoreItemAttributeCustomRepository : EfCoreRepository<MdmServiceDbContext, ItemAttribute, Guid>, IItemAttributeCustomRepository
    {
        public EfCoreItemAttributeCustomRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<bool> CreateWithExcepAsync(List<ItemAttribute> seedData)
        {
            await InsertManyAsync(seedData);
            return true;
        }
    }
}
