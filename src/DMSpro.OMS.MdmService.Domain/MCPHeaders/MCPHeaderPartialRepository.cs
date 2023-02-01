using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
	public partial interface IMCPHeaderRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}