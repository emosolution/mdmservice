using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.UOMs
{
	public partial interface IUOMRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}