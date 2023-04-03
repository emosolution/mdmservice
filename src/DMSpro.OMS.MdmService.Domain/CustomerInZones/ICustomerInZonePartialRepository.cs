using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CustomerInZones
{
    public partial interface ICustomerInZoneRepository : IRepository<CustomerInZone, Guid>
    {
        Task<List<CustomerInZone>> GetByIdAsync(List<Guid> ids);
    }
}