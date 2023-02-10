using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.UOMGroups
{
	public partial interface IUOMGroupRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);

		Task<Dictionary<string, Guid>> GetListIdByCodeAsync(List<string> codes);

		Task<int> GetCountByCodeAsync(List<string> codes);
	}
}