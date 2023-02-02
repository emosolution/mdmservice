using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.MCPDetails
{
	public partial interface IMCPDetailRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}