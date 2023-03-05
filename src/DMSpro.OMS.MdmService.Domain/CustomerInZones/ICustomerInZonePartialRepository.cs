using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace DMSpro.OMS.MdmService.CustomerInZones
{
    public partial interface ICustomerInZoneRepository
    {
        Task<IQueryable<CustomerInZoneWithNavigationProperties>> GetQueryableWithNavigationPropertiesAsync();

        Task<List<CustomerInZone>> GetByIdAsync(List<Guid> ids);

    }
}