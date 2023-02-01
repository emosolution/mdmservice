using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.VATs
{
	public partial interface IVATRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}