using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerImages
{
    public partial interface ICustomerImageRepository
    {
        Task<List<CustomerImage>> GetByIdAsync(List<Guid> ids);
    }
}
