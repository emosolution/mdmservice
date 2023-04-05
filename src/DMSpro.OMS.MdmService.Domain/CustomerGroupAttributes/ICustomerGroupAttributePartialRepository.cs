using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CustomerGroupAttributes
{
    public partial interface ICustomerGroupAttributeRepository : IRepository<CustomerGroupAttribute, Guid>
    {
        Task<List<CustomerGroupAttribute>> GetByIdAsync(List<Guid> ids);
    }
}