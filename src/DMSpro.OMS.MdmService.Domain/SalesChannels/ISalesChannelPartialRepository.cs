using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.SalesChannels
{
	public partial interface ISalesChannelRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);

		Task<Dictionary<string, Guid>> GetListIdByCodeAsync(List<string> codes);

		Task<int> GetCountByCodeAsync(List<string> codes);

		Task<bool> CheckUniqueCodeForUpdate(List<string> codes, List<Guid> ids);

		Task<List<SalesChannel>> GetByIdAsync(List<Guid> ids);
	}
}