using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.SystemConfigs
{
	public partial interface ISystemConfigRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}