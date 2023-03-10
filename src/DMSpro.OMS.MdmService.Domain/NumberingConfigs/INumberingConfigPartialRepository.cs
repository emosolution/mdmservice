using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
	public partial interface INumberingConfigRepository : IRepository<NumberingConfig, Guid>
    {
		Task<List<NumberingConfig>> GetByIdAsync(List<Guid> ids);
    }
}