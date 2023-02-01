using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
	public partial interface ISalesOrgHeaderRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}