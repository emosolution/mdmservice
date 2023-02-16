using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
	public partial interface INumberingConfigRepository
	{
		Task<List<NumberingConfig>> GetByIdAsync(List<Guid> ids);
    }
}