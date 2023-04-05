using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public partial interface ICustomerGroupListRepository : IRepository<CustomerGroupList, Guid>
    {
        Task<List<CustomerGroupList>> GetByIdAsync(List<Guid> ids);
    }
}