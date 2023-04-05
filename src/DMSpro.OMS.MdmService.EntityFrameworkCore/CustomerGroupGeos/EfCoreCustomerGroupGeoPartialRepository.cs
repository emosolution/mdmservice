using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public class EfCoreCustomerGroupGeoRepository : EfCoreRepository<MdmServiceDbContext, CustomerGroupGeo, Guid>, ICustomerGroupGeoRepository
    {
        public EfCoreCustomerGroupGeoRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task<List<CustomerGroupGeo>> GetByIdAsync(List<Guid> ids)
        {
            var items = (await GetDbSetAsync()).Where(x => ids.Contains(x.Id));
            return await items.ToListAsync();
        }
    }
}