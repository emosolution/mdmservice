using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.SystemDatas
{
	public partial interface ISystemDataRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);

		Task<Dictionary<string, Guid>> GetListIdByCodeAsync(List<string> codes);
	}
}