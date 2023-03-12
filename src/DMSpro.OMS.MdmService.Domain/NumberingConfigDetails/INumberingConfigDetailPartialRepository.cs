using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public partial interface INumberingConfigDetailRepository : 
        IRepository<NumberingConfigDetail, Guid>
    {
        Task<List<NumberingConfigDetail>> GetByIdAsync(List<Guid> ids);
    }
}