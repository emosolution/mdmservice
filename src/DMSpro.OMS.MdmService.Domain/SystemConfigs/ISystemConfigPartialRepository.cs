using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.SystemConfigs
{
	public partial interface ISystemConfigRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);

		Task<Dictionary<string, Guid>> GetListIdByCodeAsync(List<string> codes);

		Task<int> GetCountByCodeAsync(List<string> codes);
	}
}