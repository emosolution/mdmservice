using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.SalesChannels
{
	public partial interface ISalesChannelRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}