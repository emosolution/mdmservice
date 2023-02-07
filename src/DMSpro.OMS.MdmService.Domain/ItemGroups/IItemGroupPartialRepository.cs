using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemGroups
{
	public partial interface IItemGroupRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);

		Task<Dictionary<string, Guid>> GetListIdByCodeAsync(List<string> codes);
	}
}