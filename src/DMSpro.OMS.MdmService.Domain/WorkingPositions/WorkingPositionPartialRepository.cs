using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.WorkingPositions
{
	public partial interface IWorkingPositionRepository
	{
		Task<Guid?> GetIdByCodeAsync(string code);
	}
}