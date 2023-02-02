using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.SystemDatas
{
	public partial interface ISystemDataRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}