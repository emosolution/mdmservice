using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
	public partial interface INumberingConfigRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}