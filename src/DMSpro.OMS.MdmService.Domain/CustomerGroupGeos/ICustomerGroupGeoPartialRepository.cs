using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public interface ICustomerGroupGeoRepository : IRepository<CustomerGroupGeo, Guid>
    {
        Task<List<CustomerGroupGeo>> GetByIdAsync(List<Guid> ids);
    }
}