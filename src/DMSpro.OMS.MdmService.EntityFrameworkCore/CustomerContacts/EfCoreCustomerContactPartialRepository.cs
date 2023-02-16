using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
	public partial class EfCoreCustomerContactRepository
	{
		public virtual async Task<List<CustomerContact>> GetByIdAsync(List<Guid> ids)
        {
            var items = (await GetDbSetAsync()).Where(x => ids.Contains(x.Id));
            return await items.ToListAsync();
        }
    }
}