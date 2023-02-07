using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.WorkingPositions
{
	public partial interface IWorkingPositionRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);

		Task<Dictionary<string, Guid>> GetListIdByCodeAsync(List<string> codes);
	}
}