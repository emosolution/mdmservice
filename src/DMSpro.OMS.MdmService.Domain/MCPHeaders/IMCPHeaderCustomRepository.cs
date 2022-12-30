using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public interface IMCPHeaderCustomRepository : IRepository<MCPHeader, Guid>
    {
        Task<List<Guid>> GetMCPHeaderIdForScheduledVisitPlanGeneration();
    }
}