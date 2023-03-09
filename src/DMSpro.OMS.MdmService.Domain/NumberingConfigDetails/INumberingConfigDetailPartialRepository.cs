using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public partial interface INumberingConfigDetailRepository
    {
        Task<List<NumberingConfigDetail>> GetByIdAsync(List<Guid> ids);
    }
}