using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
	public partial class EfCoreCustomerGroupByGeoRepository
	{
		public virtual async Task<List<CustomerGroupByGeo>> GetByIdAsync(List<Guid> ids)
        {
            var items = (await GetDbSetAsync()).Where(x => ids.Contains(x.Id));
            return await items.ToListAsync();
        }
    }
}