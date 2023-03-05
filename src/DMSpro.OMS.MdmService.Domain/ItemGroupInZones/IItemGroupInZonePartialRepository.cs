using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemGroupInZones
{
    public partial interface IItemGroupInZoneRepository 
    {
        Task<IQueryable<ItemGroupInZoneWithNavigationProperties>> GetQueryableWithNavigationPropertiesAsync();

        Task<List<ItemGroupInZone>> GetByIdAsync(List<Guid> ids);
    }
}