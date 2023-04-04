using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
	public partial class EfCoreItemAttributeValueRepository : EfCoreRepository<MdmServiceDbContext, ItemAttributeValue, Guid>, IItemAttributeValueRepository
    {
        public EfCoreItemAttributeValueRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

		public virtual async Task<List<ItemAttributeValue>> GetByIdAsync(List<Guid> ids)
        {
            var items = (await GetDbSetAsync()).Where(x => ids.Contains(x.Id));
            return await items.ToListAsync();
        }
    }
}