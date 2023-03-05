using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DMSpro.OMS.MdmService.ItemGroupInZones
{
    public partial class EfCoreItemGroupInZoneRepository
    {
        public virtual async Task<IQueryable<ItemGroupInZoneWithNavigationProperties>> GetQueryableWithNavigationPropertiesAsync()
        {
            return await GetQueryForNavigationPropertiesAsync();
        }
        public virtual async Task<List<ItemGroupInZone>> GetByIdAsync(List<Guid> ids)
        {
            var items = (await GetDbSetAsync()).Where(x => ids.Contains(x.Id));
            return await items.ToListAsync();
        }
    }
}